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

void read_string_array(std::ifstream& f, string*& p, char* len);

void read_float_array(ifstream& f, float*& p, char* len);

void read_uint_array(ifstream& f, uint32_t*& p, char* len);

void read_long_array(ifstream& f, int64_t*& p, char* len);

void read_inner_array(ifstream&f, uint16_t*& p, char* len);

void readSeqlist(ifstream&f, char* len);

void readSeqRef(ifstream&f, uint16_t* len);

void readv2(std::ifstream& f, vec2& v);

void readv3(std::ifstream& f, vec3& v);

void readv3(std::ifstream& f, ivec3& v);

void readv4(std::ifstream& f, vec4& v);

void readintarr(std::ifstream& f, int* v, int32_t* len);

void readfloatarr(std::ifstream& f, float* v, int32_t* len);

void readdoublearr(std::ifstream& f, double* v, int32_t* len);


#endif // ! __reader__


