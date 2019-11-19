#ifndef  __reader__
#define __reader__

#include <fstream>
#include <sstream>
#include <iostream>
#include <string>
#include <stdint.h>
#include "common.h"

using namespace std;

void readstring(std::ifstream& f, string& str);

template <typename T>
void read_number_array(ifstream& f, T*& p, char* len)
{
	f.read(len, sizeof(char));
	size_t length = (size_t)(*len);
	p = new T[length];
	loop(length)
	{
		f.read((char*)(p + i), sizeof(T));
	}
}

void readSeqlist(ifstream&f, char* len);

void readSeqRef(ifstream&f, uint16_t* len);

void readintarr(std::ifstream& f, int* v, int32_t* len);

void readfloatarr(std::ifstream& f, float* v, int32_t* len);

void readdoublearr(std::ifstream& f, double* v, int32_t* len);


#endif // ! __reader__


