#ifndef  __reader__
#define __reader__

#include <fstream>
#include <sstream>
#include <iostream>
#include <string>
#include <stdint.h>
#include <codecvt> 
#include "common.h"

#ifdef _WIN32
#include <windows.h>
#endif

using namespace std;


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

extern unsigned char reader_flag;

int32_t readStringLen(std::ifstream& f);

void readstring(std::ifstream& f, string& str);

void readSeqlist(ifstream&f, char* count, char* allSameMask, uint16_t* startOffset);

void readSeqRef(ifstream&f, uint16_t* offset);

string Utf8ToGbk(char *src_str);


#endif // ! __reader__


