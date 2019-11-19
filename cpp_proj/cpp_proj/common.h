#ifndef __common__
#define __common__


const double PI = 3.14159265359;
typedef unsigned int uint;

#define max(a,b) ((a)>(b)?(a):(b))

#define min(a,b) ((a)<(b)?(a):(b))

#undef foreach
#define foreach(var, container) for( auto var = (container).begin(); var != (container).end(); ++var)

#define loop(end_l) for (int i=0;i<end_l;++i)

#define loop0i(end_l) for (size_t i=0;i<end_l;++i)

#define loop0j(end_l) for (size_t j=0;j<end_l;++j)

#define clamp(a_,b_,c_) min(max(a_,b_),c_)


#define SAFE_DELETE(ptr) \
    if(ptr != nullptr) \
    { delete ptr; ptr = nullptr; }



#endif // !__common__

