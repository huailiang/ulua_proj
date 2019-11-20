require('util')


function prt_behit( ... )
	-- body
	print("********* lua env: BeHit ***********")
	print("table length: "..#g_BeHit, util.table_len(g_BeHit))
	print(g_BeHit[1]["presentid"], g_BeHit[1]['hitid'], g_BeHit[1]['death'], #(g_BeHit[1]['hit_back']))
	print(g_BeHit[2]['presentid'], g_BeHit[2]['hitid'], g_BeHit[0]['death'], g_BeHit[2]['hit_back'][0], g_BeHit[2]['hit_back'][1])
	print(g_BeHit[17]["presentid"], g_BeHit[17]['hitid'], g_BeHit[17]['death'], #(g_BeHit[17]['hit_back']))
end


function prt_actor( ... )
	-- body
	print("********* lua env: ActorTable ***********")
	print("table length: "..#g_ActorTable, util.table_len(g_ActorTable))
	print(g_ActorTable[1]['actorId'], g_ActorTable[1]['idle'])
	print(g_ActorTable[2]['actorId'], g_ActorTable[2]['idle'])
end


