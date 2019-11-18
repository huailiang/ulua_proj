#include "behit.h"



behit::behit(string name)
{
	this->name = name;
}


behit::~behit()
{
	this->name = "";
	delete[] p_str;
	delete[] p_int32;
	delete[] p_uint32;
	delete[] p_int64;
	delete[] p_float;
	delete[] p_double;
	delete[] p_index;
}


void behit::read()
{
	ifstream ifs;
	ifs.exceptions(std::ifstream::failbit | std::ifstream::badbit);
	try
	{
		std::string path = name;
		ifs.open(path, std::ifstream::binary | std::ios::in);
		ifs.seekg(0, ios::beg);
		ifs.read((char*)&fileSize, sizeof(int32_t));
		ifs.read((char*)&lineCount, sizeof(int32_t));
		cout << "filesize:" << fileSize << " line count:" << lineCount << endl;
		this->init_header(ifs);
		this->init_column(ifs);
		ifs.close();
	}
	catch (std::ifstream::failure e)
	{
		std::cerr << "read table error " << name << std::endl;
	}
}


void behit::init_header(ifstream& f)
{
	bool hasString = false;
	f.read((char*)&hasString, sizeof(bool));
	f.read((char*)&strCount, sizeof(int16_t));
	cout << "hasString:" << hasString << " stringCount:" << strCount << endl;
	p_str = new string[strCount];
	loop(strCount)
	{
		readstring(f, p_str[i]);
		cout << i << " " << p_str[i] << endl;
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

void behit::init_column(ifstream& f)
{
	f.read(&columnCount, sizeof(char));
	cout << "columnCount: " << (int)columnCount << "  lineCount:" << lineCount << endl;
	loop(columnCount)
	{
		char type0, type1;
		f.read(&type0, sizeof(char));
		f.read(&type1, sizeof(char));
	}
	loop(lineCount)
	{
		int32_t size = 0;
		f.read((char*)&size, sizeof(int32_t));
		int32_t beforePos = f.tellg();
		this->read_line(f);
		int32_t afterPos = f.tellg();
		cout << i << ": before pos:" << beforePos << " after pos:" << afterPos << " size:" << size << endl;
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
}


void behit::read_line(ifstream& f)
{
	uint32_t presentid = 0;
	f.read((char*)&presentid, sizeof(presentid));
	int32_t hitid = 0;
	f.read((char*)&hitid, sizeof(int32_t));
	uint16_t *hit_front = nullptr, *hit_front_curve = nullptr, *hit_back = nullptr, *hit_back_curve = nullptr;
	uint16_t *hit_left = nullptr, *hit_left_curve = nullptr, *hit_right = nullptr, *hit_right_curve = nullptr;
	uint16_t *death_curve = nullptr;
	float *cameraShake = nullptr, *postEffectParams = nullptr;
	uint32_t *qte = nullptr;

	char len = 0;
	uint16_t ulen = 0;
	read_inner_array(f, hit_front, &len);
	read_inner_array(f, hit_front_curve, &len);
	read_inner_array(f, hit_back, &len);
	read_inner_array(f, hit_back_curve, &len);
	read_inner_array(f, hit_left, &len);
	read_inner_array(f, hit_left_curve, &len);
	read_inner_array(f, hit_right, &len);
	read_inner_array(f, hit_right_curve, &len);
	string death = inner_string(f);
	read_inner_array(f, death_curve, &len);
	read_float_array(f, cameraShake, &len);
	read_uint_array(f, qte, &len);
	readSeqlist(f, &len); // QTETime
	int32_t setFace, postEffect, PVESP, PVPSP;
	f.read((char*)&setFace, sizeof(int32_t));
	f.read((char*)&postEffect, sizeof(int32_t));
	read_float_array(f, postEffectParams, &len);
	readSeqlist(f, &len); // BuffIDs
	readSeqlist(f, &len); // BuffTime
	f.read((char*)&PVESP, sizeof(int32_t));
	f.read((char*)&PVPSP, sizeof(int32_t));
	readSeqRef(f, &ulen); // PostEffectTime
	readSeqRef(f, &ulen); // ChangeFlyTime
	bool setHitFromDir = false;
	f.read((char*)&setHitFromDir, sizeof(bool));
	cout << "pve:" << PVESP << " pvp:" << PVPSP << " " << setHitFromDir << endl;
}

string behit::inner_string(ifstream& f)
{
	uint16_t idx;
	f.read((char*)&idx, sizeof(uint16_t));
	return p_str[idx];
}