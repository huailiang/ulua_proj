#ifndef __table__
#define __table__

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <functional>
#include "reader.h"
#include "cvs.h"

using namespace std;



class Behit
{

	typedef void(Behit::*fReader)(ifstream& f, int row);

public:
	Behit(string name);
	~Behit();
	void Read();

private:
	void PostProcess();
	void ReadHeader(ifstream& f);
	void ReadContent(ifstream& f);
	void ReadLine(ifstream& f, int i);
	void ReadLine2(ifstream& f, int i);
	string InnerString(ifstream& f);

private:
	void ReadUint32(ifstream& f, int row);
	void ReadInt32(ifstream& f, int row);
	void ReadUint16(ifstream& f, int row);
	void ReadInt64(ifstream& f, int row);
	void ReadFloat(ifstream& f, int row);
	void ReadDouble(ifstream& f, int row);
	void ReadBool(ifstream& f, int row);
	void ReadString(ifstream& f, int row);

	void ReadInt32Array(ifstream& f, int row);
	void ReadUint32Array(ifstream& f, int row);
	void ReadFloatArray(ifstream& f, int row);
	void ReadDoubleArray(ifstream& f, int row);
	void ReadStringArray(ifstream& f, int row);


	void ReadSeq(ifstream& f, int row);
	void ReadSeqList(ifstream& f, int row);
	

private:
	string name;
	char columnCount;
	int32_t fileSize = 0, lineCount = 0;
	uint16_t strCount, intCount, uintCount, longCount, floatCount, doubleCount, idxCount;
	string* p_str = NULL;
	int32_t* p_int32 = NULL;
	uint32_t* p_uint32 = NULL;
	int64_t* p_int64 = NULL;
	float* p_float = NULL;
	double* p_double = NULL;
	uint16_t* p_index = NULL;
	int p_curr = 0;

private:
	fReader pReader[40];
	string* headers; //表格header = 列数 (这里由lua传过来)
	int* types;
	cvs* p_cvs;
};


#endif // !__table__




