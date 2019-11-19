#ifndef __table__
#define __table__

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include "reader.h"
#include "cvs.h"

using namespace std;

class Behit
{
public:
	Behit(string name);
	~Behit();
	void Read();

private:
	void ReadHeader(ifstream& f);
	void ReadContent(ifstream& f);
	void ReadLine(ifstream& f);
	string InnerString(ifstream& f);

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

private:
	string* headers; //表格header = 列数 (这里由lua传过来)
};


#endif // !__table__




