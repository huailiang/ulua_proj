str = "I am so cool" 

tbl = {id =12, name = "shun", age = 20114442, tb2 ={ name="sunnt"} }  

global_c_read_table = {integer_val = 112,double_val = 2.34,string_val = "test_string"}

print(tbl.id)

function add(a,b)  
    return a + b  
end

function average(...)
   result = 0
   local arg={...}
   for i,v in ipairs(arg) do
      result = result + v
   end
   print("arg count " .. #arg .. " .")
   return result/#arg, result
end

print("hello world from lua")

local ave,sum = average(10,20,30)
print("ave : ",ave," sum : ",sum)

-- print("lua=>"..global_c_write_val)
print(os.date())
print(os.clock())
print(os.tmpname())
print("*********************")