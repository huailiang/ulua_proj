#include "reader.h"


int32_t readStringLen(std::ifstream& f)
{
	// Read out an Int32 7 bits at a time.  The high bit
	// of the byte when on means to continue reading more bytes.
	int32_t count = 0;
	int shift = 0;
	char b;
	do
	{
		if (shift == 5 * 7) // 5 bytes max per Int32, shift += 7
		{
			std::cerr << "string  Format_Bad7BitInt32" << endl;
			return 0;
		}
		f.read(&b, sizeof(char));
		count |= (b & 0x7F) << shift;
		shift += 7;
	} while ((b & 0x80) != 0);

	return count;
}


void readstring(std::ifstream& f, std::string& str)
{
	int32_t len = readStringLen(f);
	if (len > 0)
	{
		char* temp = new char[len + 1];
		f.read(temp, len);
		temp[len] = '\0';
		str = Utf8ToGbk(temp);
		delete[] temp;
	}
}


void readSeqlist(ifstream&f, char* count, char* allSameMask, uint16_t* startOffset)
{
	f.read(count, sizeof(char));
	if ((*count) > 0)
	{
		f.read(allSameMask, sizeof(char));
		f.read((char*)startOffset, sizeof(uint16_t));
	}
}


void readSeqRef(ifstream&f, uint16_t* offset)
{
	f.read((char*)offset, sizeof(uint16_t));
}


/*
不同的国家和地区制定了不同的标准，由此产生了 GB2312、GBK、Big5、Shift_JIS 等各自的编码标准
这些使用 1 至 4 个字节来代表一个字符的各种汉字延伸编码方式，称为 ANSI 编码
在简体中文Windows操作系统中，ANSI 编码代表 GBK 编码,在日文Windows操作系统中，ANSI 编码代表 Shift_JIS 编码


非windows使用iconv 涉及到平台编译问题， 这里没有移植进去

参考：
	https://blog.csdn.net/u012234115/article/details/83186386
	https://www.cnblogs.com/wangbin/p/6744352.html

*/
string Utf8ToGbk(char *src_str)
{
#ifdef _WIN32
	int len = MultiByteToWideChar(CP_UTF8, 0, src_str, -1, NULL, 0);
	wchar_t* wszGBK = new wchar_t[len + 1];
	memset(wszGBK, 0, len * 2 + 2);
	MultiByteToWideChar(CP_UTF8, 0, src_str, -1, wszGBK, len);
	len = WideCharToMultiByte(CP_ACP, 0, wszGBK, -1, NULL, 0, NULL, NULL);
	char* szGBK = new char[len + 1];
	memset(szGBK, 0, len + 1);
	WideCharToMultiByte(CP_ACP, 0, wszGBK, -1, szGBK, len, NULL, NULL);
	string strTemp(szGBK);
	if (wszGBK) delete[] wszGBK;
	if (szGBK) delete[] szGBK;
	return strTemp;
#else
	return src_str;
#endif
}

