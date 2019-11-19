#include "behit.h"



Behit::Behit(string name)
{
	this->name = name;
	headers = new string[25]{
		"presentid",  // 0
		"hitid",
		"hit_front",
		"hit_front_curve",
		"hit_back",   // 4
		"hit_back_curve",
		"hit_left",
		"hit_left_curve",
		"hit_right",  // 8
		"hit_right_curve",
		"death",
		"death_curve",
		"cameraShake",  // 12
		"qte",
		"QTETime",
		"setFace",
		"postEffect", // 16
		"postEffectParams",
		"BuffIDs",
		"BuffTime",
		"PVESP", // 20
		"PVPSP",
		"PostEffectTime",
		"ChangeFlyTime",
		"setHitFromDir" //24
	};
}


Behit::~Behit()
{
	this->name = "";
	delete[] p_str;
	delete[] p_int32;
	delete[] p_uint32;
	delete[] p_int64;
	delete[] p_float;
	delete[] p_double;
	delete[] p_index;
	delete[] headers;
	SAFE_DELETE(p_cvs);
}


void Behit::Read()
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
		this->ReadHeader(ifs);
		this->ReadContent(ifs);
		ifs.close();
	}
	catch (std::ifstream::failure e)
	{
		std::cerr << "read table error " << name << std::endl;
	}
}


void Behit::ReadHeader(ifstream& f)
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


void Behit::ReadContent(ifstream & f)
{
	f.read(&columnCount, sizeof(char));
	cout << "columnCount: " << (int)columnCount << "  lineCount:" << lineCount <<" name:"<<name<< endl;
	p_cvs = new cvs("c_behit", lineCount, columnCount, headers);
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
	PostProcess();
}

void Behit::ReadLine(ifstream& f, int i)
{
	uint32_t presentid = 0;
	f.read((char*)&presentid, sizeof(presentid));
	p_cvs->fill(i, 0, presentid);
	int32_t hitid = 0;
	f.read((char*)&hitid, sizeof(int32_t));
	p_cvs->fill(i, 1, hitid);
	uint16_t *hit_front = nullptr, *hit_front_curve = nullptr, *hit_back = nullptr, *hit_back_curve = nullptr;
	uint16_t *hit_left = nullptr, *hit_left_curve = nullptr, *hit_right = nullptr, *hit_right_curve = nullptr;
	uint16_t *death_curve = nullptr;
	float *cameraShake = nullptr, *postEffectParams = nullptr;
	uint32_t *qte = nullptr;

	char len = 0;
	uint16_t ulen = 0;
	read_inner_array(f, hit_front, &len);
	p_cvs->fill(i, 2, hit_front, len);
	read_inner_array(f, hit_front_curve, &len);
	p_cvs->fill(i, 3, hit_front_curve, len);
	read_inner_array(f, hit_back, &len);
	p_cvs->fill(i, 4, hit_back, len);
	read_inner_array(f, hit_back_curve, &len);
	p_cvs->fill(i, 5, hit_back_curve, len);
	read_inner_array(f, hit_left, &len);
	p_cvs->fill(i, 6, hit_left, len);
	read_inner_array(f, hit_left_curve, &len);
	p_cvs->fill(i, 7, hit_left_curve, len);
	read_inner_array(f, hit_right, &len);
	p_cvs->fill(i, 8, hit_right, len);
	read_inner_array(f, hit_right_curve, &len);
	p_cvs->fill(i, 9, hit_right_curve, len);
	string death = InnerString(f);
	p_cvs->fill(i, 10, death);
	read_inner_array(f, death_curve, &len);
	p_cvs->fill(i, 11, death_curve, len);
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
}

string Behit::InnerString(ifstream& f)
{
	uint16_t idx;
	f.read((char*)&idx, sizeof(uint16_t));
	return p_str[idx];
}

void Behit::PostProcess()
{
	auto L = p_cvs->GetLuaL();
	luaL_dofile(L, "behit.lua");
	lua_getglobal(L, "prt_behit");
	lua_pcall(L, 0, 0, 0);
}