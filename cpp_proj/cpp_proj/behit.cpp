#include "behit.h"
#include "xtype.h"



Behit::Behit(string name)
{
	this->name = name;

	this->columnCount = 25;

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

	types = new int[25]{
		UINT32,  // presentid
		INT32,  // hitid
		STRING_ARR, // hit_front
		STRING_ARR, // hit_front_curve
		STRING_ARR,
		STRING_ARR,
		STRING_ARR,
		STRING_ARR, // hit_left_curve
		STRING_ARR,
		STRING_ARR, // hit_right_curve
		STRING,  // death
		STRING_ARR, // death_curve
		FLOAT_ARR,
		UINT32_ARR, // qte
		FLOAT_LIST,
		INT32,
		INT32,
		FLOAT_ARR, // postEffectParams
		INT32_LIST,
		FLOAT_LIST,
		INT32,
		INT32, //PVPSP
		FLOAT_SEQ,
		FLOAT_SEQ, // ChangeFlyTime
		BOOLEAN
	};

	void(Behit::*fReader)(ifstream& f, int row) = &Behit::ReadUint32;

	pReader[INT32] = &Behit::ReadInt32;
	pReader[UINT32] = &Behit::ReadUint32;
	pReader[UINT16] = &Behit::ReadUint16;
	pReader[INT64] = &Behit::ReadUint16;
	pReader[FLOAT] = &Behit::ReadFloat;
	pReader[DOUBLE] = &Behit::ReadDouble;
	pReader[BOOLEAN] = &Behit::ReadBool;
	pReader[STRING] = &Behit::ReadString;

	pReader[INT32_ARR] = &Behit::ReadInt32Array;
	pReader[UINT32_ARR] = &Behit::ReadUint32Array;
	pReader[FLOAT_ARR] = &Behit::ReadFloatArray;
	pReader[DOUBLE_ARR] = &Behit::ReadDoubleArray;
	pReader[STRING_ARR] = &Behit::ReadStringArray;

	pReader[INT32_SEQ] = &Behit::ReadSeq;
	pReader[UINT32_SEQ] = &Behit::ReadSeq;
	pReader[FLOAT_SEQ] = &Behit::ReadSeq;
	pReader[DOUBLE_SEQ] = &Behit::ReadSeq;
	pReader[STRING_SEQ] = &Behit::ReadSeq;

	pReader[INT32_LIST] = &Behit::ReadSeqList;
	pReader[UINT32_LIST] = &Behit::ReadSeqList;
	pReader[FLOAT_LIST] = &Behit::ReadSeqList;
	pReader[DOUBLE_LIST] = &Behit::ReadSeqList;
	pReader[STRING_LIST] = &Behit::ReadSeqList;
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


void Behit::ReadUint32(ifstream& f, int row)
{
	uint32_t tmp =0;
	f.read((char*)&tmp, sizeof(uint32_t));
	p_cvs->fill(row, p_curr++, tmp);
}

void Behit::ReadInt32(ifstream& f, int row)
{
	int32_t tmp=0;
	f.read((char*)&tmp, sizeof(int32_t));
	p_cvs->fill(row, p_curr++, tmp);
}

void Behit::ReadUint16(ifstream& f, int row)
{
	uint16_t tmp=0;
	f.read((char*)&tmp, sizeof(uint16_t));
	p_cvs->fill(row, p_curr++, tmp);
}

void Behit::ReadInt64(ifstream& f, int row)
{
	int64_t tmp = 0;
	f.read((char*)&tmp, sizeof(int64_t));
	p_cvs->fill(row, p_curr++, tmp);
}

void Behit::ReadFloat(ifstream& f, int row)
{
	float tmp = 0;
	f.read((char*)&tmp, sizeof(float));
	p_cvs->fill(row, p_curr++, tmp);
}

void Behit::ReadDouble(ifstream& f, int row)
{
	double tmp = 0;
	f.read((char*)&tmp, sizeof(double));
	p_cvs->fill(row, p_curr++, tmp);
}

void Behit::ReadBool(ifstream& f, int row)
{
	bool tmp = 0;
	f.read((char*)&tmp, sizeof(bool));
	p_cvs->fill(row, p_curr++, tmp);
}

void Behit::ReadString(ifstream& f, int row)
{
	string tmp = InnerString(f);
	p_cvs->fill(row, p_curr++, tmp);
}

void Behit::ReadInt32Array(ifstream& f, int row)
{
	int32_t *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);
	p_cvs->fill(row, p_curr++, tmp, (size_t)len);
	delete[] tmp;
}

void Behit::ReadUint32Array(ifstream& f, int row)
{
	uint32_t *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);
	p_cvs->fill(row, p_curr++, tmp, (size_t)len);
	delete[] tmp;
}

void Behit::ReadFloatArray(ifstream& f, int row)
{
	float *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);
	p_cvs->fill(row, p_curr++, tmp, (size_t)len);
	delete[] tmp;
}

void Behit::ReadDoubleArray(ifstream& f, int row)
{
	double *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);
	p_cvs->fill(row, p_curr++, tmp, (size_t)len);
	delete[] tmp;
}

void Behit::ReadStringArray(ifstream& f, int row)
{
	uint16_t *tmp = nullptr;
	char len = 0;
	read_number_array(f, tmp, &len);

	int size = (size_t)len;
	string* ptr = new string[size];
	loop(size)
	{
		auto idx = tmp[i];
		*(ptr + i) = p_str[idx];
	}
	p_cvs->fill(row, p_curr++, ptr, (size_t)len);
	delete[] tmp;
	delete[] ptr;
}


void Behit::ReadSeq(ifstream& f, int row)
{
	uint16_t len = 0;
	readSeqRef(f, &len);
	p_curr++;
}

void Behit::ReadSeqList(ifstream& f, int row)
{
	char len = 0;
	readSeqlist(f, &len);
	p_curr++;
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
	cout << "columnCount: " << (int)columnCount << "  lineCount:" << lineCount << " name:" << name << endl;
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

void Behit::ReadLine2(ifstream& f, int i)
{
	loop(columnCount)
	{
		fReader reader = pReader[this->types[i]];
		(this->*reader)(f, i);
	}
}

void Behit::ReadLine(ifstream& f, int i)
{
	p_curr = 0;
	uint32_t presentid = 0;
	f.read((char*)&presentid, sizeof(uint32_t));
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
	read_number_array(f, hit_front, &len);
	p_cvs->fill(i, 2, hit_front, len);
	read_number_array(f, hit_front_curve, &len);
	p_cvs->fill(i, 3, hit_front_curve, len);
	read_number_array(f, hit_back, &len);
	p_cvs->fill(i, 4, hit_back, len);
	read_number_array(f, hit_back_curve, &len);
	p_cvs->fill(i, 5, hit_back_curve, len);
	read_number_array(f, hit_left, &len);
	p_cvs->fill(i, 6, hit_left, len);
	read_number_array(f, hit_left_curve, &len);
	p_cvs->fill(i, 7, hit_left_curve, len);
	read_number_array(f, hit_right, &len);
	p_cvs->fill(i, 8, hit_right, len);
	read_number_array(f, hit_right_curve, &len);
	p_cvs->fill(i, 9, hit_right_curve, len);
	string death = InnerString(f);
	p_cvs->fill(i, 10, death);
	read_number_array(f, death_curve, &len);
	p_cvs->fill(i, 11, death_curve, len);
	read_number_array(f, cameraShake, &len);
	read_number_array(f, qte, &len);
	readSeqlist(f, &len); // QTETime
	int32_t setFace, postEffect, PVESP, PVPSP;
	f.read((char*)&setFace, sizeof(int32_t));
	f.read((char*)&postEffect, sizeof(int32_t));
	read_number_array(f, postEffectParams, &len);
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