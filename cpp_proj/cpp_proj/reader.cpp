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


void read_string_array(ifstream& f, string* p, char* len)
{
	f.read(len, sizeof(char));
	size_t length = (size_t)(*len);
	p = new string[length];
	loop(length)
	{
		readstring(f, p[i]);
	}
}

void read_float_array(ifstream& f, float* p, char* len)
{
	f.read(len, sizeof(char));
	size_t length = (size_t)(*len);
	p = new float[length];
	loop(length)
	{
		f.read((char*)(p + i), sizeof(float));
	}
}

void read_uint_array(ifstream& f, uint32_t* p, char* len)
{
	f.read(len, sizeof(char));
	size_t length = (size_t)(*len);
	p = new uint32_t[length];
	loop(length)
	{
		f.read((char*)(p + i), sizeof(uint32_t));
	}
}

void read_long_array(ifstream& f, int64_t* p, char* len)
{
	f.read(len, sizeof(char));
	size_t length = (size_t)(*len);
	p = new int64_t[length];
	loop(length)
	{
		f.read((char*)(p + i), sizeof(int64_t));
	}
}

void read_inner_array(ifstream&f, uint16_t* p, char* len)
{
	f.read(len, sizeof(char));
	size_t length = (size_t)(*len);
	p = new uint16_t[length];
	loop(length)
	{
		f.read((char*)(p + i), sizeof(uint16_t));
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

void readv2(std::ifstream& f, vec2& v)
{
	f.read((char*)&v.x, sizeof(float));
	f.read((char*)&v.y, sizeof(float));
}

void readv3(std::ifstream& f, vec3& v)
{
	f.read((char*)&v.x, sizeof(float));
	f.read((char*)&v.y, sizeof(float));
	f.read((char*)&v.z, sizeof(float));
}

void readv3(std::ifstream& f, ivec3& v)
{
	f.read((char*)&v.x, sizeof(int32_t));
	f.read((char*)&v.y, sizeof(int32_t));
	f.read((char*)&v.z, sizeof(int32_t));
}

void readv4(std::ifstream& f, vec4& v)
{
	f.read((char*)&v.x, sizeof(float));
	f.read((char*)&v.y, sizeof(float));
	f.read((char*)&v.z, sizeof(float));
	f.read((char*)&v.w, sizeof(float));
}

void readintarr(std::ifstream& f, int* v, int32_t* len)
{
	f.read((char*)len, sizeof(int));
	loop(*len)
	{
		f.read((char*)(v + i), sizeof(int));
	}
}

void readfloatarr(std::ifstream& f, float* v, int32_t* len)
{
	f.read((char*)len, sizeof(int));
	loop(*len)
	{
		f.read((char*)(v + i), sizeof(float));
	}
}


void readdoublearr(std::ifstream& f, double* v, int32_t* len)
{
	f.read((char*)len, sizeof(int));
	loop(*len)
	{
		f.read((char*)(v + i), sizeof(double));
	}
}