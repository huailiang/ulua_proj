#include "xtable.h"
#include "xtype.h"



XTable::XTable(string name, string* headers, int* types, char len)
{
	this->name = name;
	this->columnCount = len;
	this->headers = headers;
	this->types = types;

	pReader[INT32] = &XTable::ReadInt32;
	pReader[UINT32] = &XTable::ReadUint32;
	pReader[UINT16] = &XTable::ReadUint16;
	pReader[INT64] = &XTable::ReadUint16;
	pReader[FLOAT] = &XTable::ReadFloat;
	pReader[DOUBLE] = &XTable::ReadDouble;
	pReader[BOOLEAN] = &XTable::ReadBool;
	pReader[STRING] = &XTable::ReadString;

	pReader[INT32_ARR] = &XTable::ReadInt32Array;
	pReader[UINT32_ARR] = &XTable::ReadUint32Array;
	pReader[FLOAT_ARR] = &XTable::ReadFloatArray;
	pReader[DOUBLE_ARR] = &XTable::ReadDoubleArray;
	pReader[STRING_ARR] = &XTable::ReadStringArray;

	pReader[INT32_SEQ] = &XTable::ReadSeq;
	pReader[UINT32_SEQ] = &XTable::ReadSeq;
	pReader[FLOAT_SEQ] = &XTable::ReadSeq;
	pReader[DOUBLE_SEQ] = &XTable::ReadSeq;
	pReader[STRING_SEQ] = &XTable::ReadSeq;

	pReader[INT32_LIST] = &XTable::ReadSeqList;
	pReader[UINT32_LIST] = &XTable::ReadSeqList;
	pReader[FLOAT_LIST] = &XTable::ReadSeqList;
	pReader[DOUBLE_LIST] = &XTable::ReadSeqList;
	pReader[STRING_LIST] = &XTable::ReadSeqList;
}

XTable::~XTable()
{
	delete[] p_str;
	delete[] p_int32;
	delete[] p_uint32;
	delete[] p_int64;
	delete[] p_float;
	delete[] p_double;
	delete[] p_index;
	delete[] headers;
	delete[] types;
	SAFE_DELETE(p_cvs);
}

void XTable::ReadUint32(ifstream& f, int row)
{
	uint32_t tmp =0;
	f.read((char*)&tmp, sizeof(uint32_t));
	p_cvs->fill(row, p_curr++, tmp);
}

void XTable::ReadInt32(ifstream& f, int row)
{
	int32_t tmp=0;
	f.read((char*)&tmp, sizeof(int32_t));
	p_cvs->fill(row, p_curr++, tmp);
}

void XTable::ReadUint16(ifstream& f, int row)
{
	uint16_t tmp=0;
	f.read((char*)&tmp, sizeof(uint16_t));
	p_cvs->fill(row, p_curr++, tmp);
}

void XTable::ReadInt64(ifstream& f, int row)
{
	int64_t tmp = 0;
	f.read((char*)&tmp, sizeof(int64_t));
	p_cvs->fill(row, p_curr++, tmp);
}

void XTable::ReadFloat(ifstream& f, int row)
{
	float tmp = 0;
	f.read((char*)&tmp, sizeof(float));
	p_cvs->fill(row, p_curr++, tmp);
}

void XTable::ReadDouble(ifstream& f, int row)
{
	double tmp = 0;
	f.read((char*)&tmp, sizeof(double));
	p_cvs->fill(row, p_curr++, tmp);
}

void XTable::ReadBool(ifstream& f, int row)
{
	bool tmp = 0;
	f.read((char*)&tmp, sizeof(bool));
	p_cvs->fill(row, p_curr++, tmp);
}

void XTable::ReadString(ifstream& f, int row)
{
	string tmp = InnerString(f);
	p_cvs->fill(row, p_curr++, tmp);
}

void XTable::ReadInt32Array(ifstream& f, int row)
{
	int32_t *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);
	p_cvs->fill(row, p_curr++, tmp, (size_t)len);
	delete[] tmp;
}

void XTable::ReadUint32Array(ifstream& f, int row)
{
	uint32_t *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);
	p_cvs->fill(row, p_curr++, tmp, (size_t)len);
	delete[] tmp;
}

void XTable::ReadFloatArray(ifstream& f, int row)
{
	float *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);
	p_cvs->fill(row, p_curr++, tmp, (size_t)len);
	delete[] tmp;
}

void XTable::ReadDoubleArray(ifstream& f, int row)
{
	double *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);
	p_cvs->fill(row, p_curr++, tmp, (size_t)len);
	delete[] tmp;
}

void XTable::ReadStringArray(ifstream& f, int row)
{
	uint16_t *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);

	int size = (size_t)len;
	string* ptr = new string[size];
	loop(size)
	{
		auto idx = tmp[i];
		*(ptr + i) = p_str[idx];
	}
	p_cvs->fill(row, p_curr++, ptr, (size_t)len);
	delete[] tmp;
	delete[] ptr;
}

void XTable::ReadSeq(ifstream& f, int row)
{
	uint16_t len = 0;
	readSeqRef(f, &len);
	p_curr++;
}

void XTable::ReadSeqList(ifstream& f, int row)
{
	char len = 0;
	readSeqlist(f, &len);
	p_curr++;
}

void XTable::Read(lua_State* L)
{
	ifstream ifs;
	ifs.exceptions(std::ifstream::failbit | std::ifstream::badbit);
	try
	{
		std::string path = name+".bytes";
		ifs.open(path, std::ifstream::binary | std::ios::in);
		ifs.seekg(0, ios::beg);
		ifs.read((char*)&fileSize, sizeof(int32_t));
		ifs.read((char*)&lineCount, sizeof(int32_t));

		p_cvs = new cvs("g_" + name, lineCount, columnCount, headers);
		p_cvs->begin(L);

		this->ReadHeader(ifs);
		this->ReadContent(ifs);
		ifs.close();
	}
	catch (std::ifstream::failure e)
	{
		std::cerr << "read table error " << name << std::endl;
	}
}

void XTable::ReadHeader(ifstream& f)
{
	bool hasString = false;
	f.read((char*)&hasString, sizeof(bool));
	f.read((char*)&strCount, sizeof(int16_t));
	p_str = new string[strCount];
	loop(strCount)
	{
		readstring(f, p_str[i]);
	}
	f.read((char*)&intCount, sizeof(uint16_t));
	p_int32 = new int32_t[intCount];
	loop(intCount)
	{
		f.read((char*)(p_int32 + i), sizeof(int32_t));
	}
	f.read((char*)&uintCount, sizeof(uint16_t));
	p_uint32 = new uint32_t[uintCount];
	loop(uintCount)
	{
		f.read((char*)(p_uint32 + i), sizeof(uint32_t));
	}
	f.read((char*)&longCount, sizeof(uint16_t));
	p_int64 = new int64_t[longCount];
	loop(longCount)
	{
		f.read((char*)(p_int64 + i), sizeof(int64_t));
	}
	f.read((char*)&floatCount, sizeof(uint16_t));
	p_float = new float[floatCount];
	loop(floatCount)
	{
		f.read((char*)(p_float + i), sizeof(float));
	}
	f.read((char*)&doubleCount, sizeof(uint16_t));
	p_double = new double[doubleCount];
	loop(doubleCount)
	{
		f.read((char*)(p_double + i), sizeof(double));
	}
	f.read((char*)&idxCount, sizeof(uint16_t));
	p_index = new uint16_t[idxCount];
	loop(idxCount)
	{
		f.read((char*)(p_index + i), sizeof(uint16_t));
	}
}

void XTable::ReadContent(ifstream & f)
{
	f.read(&columnCount, sizeof(char));
#ifdef _DEBUG
	cout << "columnCount: " << (int)columnCount << "  lineCount:" << lineCount << " name:" << name << endl;
#endif
	
	loop(columnCount)
	{
		char type0, type1;
		f.read(&type0, sizeof(char));
		f.read(&type1, sizeof(char));
	}
	loop(lineCount)
	{
		int32_t size = 0;
		p_cvs->begin_row();
		f.read((char*)&size, sizeof(int32_t));
		auto beforePos = f.tellg();
		this->ReadLine(f, i);
		p_cvs->end_row(i);
		auto afterPos = f.tellg();
		auto delta = afterPos - beforePos;
		if (size > delta)
		{
			f.seekg(size - delta, SEEK_CUR);
		}
		else if (size < delta)
		{
			cerr << "read table error: " << this->name << endl;
			break;
		}
	}
	p_cvs->end();
}

void XTable::ReadLine(ifstream& f, int i)
{
	p_curr = 0;
	loop(columnCount)
	{
		fReader reader = pReader[this->types[i]];
		(this->*reader)(f, i);
	}
}

string XTable::InnerString(ifstream& f)
{
	uint16_t idx;
	f.read((char*)&idx, sizeof(uint16_t));
	return p_str[idx];
}
