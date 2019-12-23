#ifndef __table__
#define __table__

#include <iostream>
#include <fstream>
#include <string>
#include "reader.h"
#include "cvs.h"

using namespace std;


class XTable
{

	typedef void(XTable::*fReader)(ifstream& f, int row);

public:
	XTable(string name, string directory, string* headers, int* types, char len);
	~XTable();
	void Read(lua_State* L);

private:
	void ReadHeader(ifstream& f);
	void ReadContent(ifstream& f);
	void ReadLine(ifstream& f, int i);

private:
	void ReadUint32(ifstream& f, int row);
	void ReadInt32(ifstream& f, int row);
	void ReadInt16(ifstream& f, int row);
	void ReadInt64(ifstream& f, int row);
	void ReadFloat(ifstream& f, int row);
	void ReadDouble(ifstream& f, int row);
	void ReadBool(ifstream& f, int row);
	void ReadString(ifstream& f, int row);
	void ReadByte(ifstream& f, int row);

	void ReadInt32Array(ifstream& f, int row);
	void ReadUint32Array(ifstream& f, int row);
	void ReadFloatArray(ifstream& f, int row);
	void ReadDoubleArray(ifstream& f, int row);
	void ReadStringArray(ifstream& f, int row);

	void ReadInt32Seq(ifstream& f, int row);
	void ReadUint32Seq(ifstream& f, int row);
	void ReadFloatSeq(ifstream& f, int row);
	void ReadDoubleSeq(ifstream& f, int row);
	void ReadStringSeq(ifstream& f, int row);

	void ReadInt32List(ifstream& f, int row);
	void ReadUint32List(ifstream& f, int row);
	void ReadFloatList(ifstream& f, int row);
	void ReadDoubleList(ifstream& f, int row);
	void ReadStringList(ifstream& f, int row);


	string InnerString(ifstream& f);

private:
	string name, directory;
	char columnCount;
	int32_t fileSize, lineCount;
	uint16_t strCount, intCount, uintCount, longCount, floatCount, doubleCount, idxCount;
	string* p_str;
	int32_t* p_int32;
	uint32_t* p_uint32;
	int64_t* p_int64;
	float* p_float;
	double* p_double;
	uint16_t* p_index;
	int p_curr;

private:
	fReader pReader[40];
	string* headers; //passed by lua
	int* types;
	cvs* p_cvs;
};


#endif // !__table__




