#include "reader.h"

void readstring(std::ifstream& f, std::string& str)
{
	char len;
	f.read(&len, sizeof(char));
	if (len > 0)
	{
		char* temp = new char[len + 1];
		f.read(temp, len);
		temp[len] = '\0';
		str = temp;
		delete[]temp;
	}
}

void readSeqlist(ifstream&f, char* len)
{
	f.read(len, sizeof(char));
	char allSameMask = 1;
	uint16_t startOffset = 0;
	if ((*len) > 0)
	{
		f.read(&allSameMask, sizeof(char));
		f.read((char*)&startOffset, sizeof(uint16_t));
	}
}

void readSeqRef(ifstream&f, uint16_t* len)
{
	f.read((char*)len, sizeof(uint16_t));
}

