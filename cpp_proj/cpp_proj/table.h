#ifndef __table__
#define __table__

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include "reader.h"

using namespace std;

class table
{
public:
	table(string name);
	~table();
	void read();

private:
	void init_header(ifstream& f);
	void init_column(ifstream& f);
	void read_line(ifstream& f);

	string inner_string(ifstream& f);

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
};


#endif // !__table__




