str = "I am so cool" 

tbl = {name = "shun", age = 20114442, tb2 ={ name="sunnt"} }  

global_c_read_table = {integer_val = 112,double_val = 2.34,string_val = "test_string"}

function add(a,b)  
    return a + b  
end


print("hello world from lua")

-- average 在c++中实现
local ave,sum = average(10,20,30)
print("ave : ",ave," sum : ",sum)

-- global_c_write_val在c++中定义
print("lua=>"..global_c_write_val)
print(os.date())
print(os.clock())
print(os.tmpname())
print("*********************")