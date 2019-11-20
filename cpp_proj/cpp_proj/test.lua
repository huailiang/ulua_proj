require('util')

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

function prt_ctable( ... )
	-- body
	print("table length: "..#global_c_write, util.table_len(global_c_write))
	print(global_c_write[101])
	print(global_c_write["baidu"])
end

function table_v2( ... )
	-- body
	print("***********")
	print("table length: ", util.table_len(m_table), #m_table)
	print(m_table[0][100])
	print(m_table[1][101])
	print(m_table[1]["abc"])
	print(m_table[1]["arr"][0])
	print(m_table[1]["arr"][1])
	print(util.table_len(m_table[1]['arr']))
end

print("hello world from lua")
