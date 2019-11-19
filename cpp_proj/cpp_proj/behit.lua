require('util')


function prt_behit( ... )
	-- body
	print("table length: "..#c_behit, util.table_len(c_behit))
	print(c_behit[0]['presentid'], c_behit[0]['hitid'], c_behit[0]['death'], #(c_behit[0]['hit_back']))
	print(c_behit[1]["presentid"], c_behit[1]['hitid'], c_behit[1]['death'], #(c_behit[1]['hit_back']))
	print(c_behit[17]["presentid"], c_behit[17]['hitid'], c_behit[17]['death'], #(c_behit[17]['hit_back']))
end


print("read table behit from lua")
