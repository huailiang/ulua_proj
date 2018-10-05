#!/bin/bash

currdir=`cd $(dirname $0); pwd`

luadir=${currdir}"/../Assets/uLua/Resources/Lua/Hotfix"  

luac=${currdir}"/install-32/bin/luac"


function makeluadir(){
    for element in `ls $1`
    do  
        dir_or_file=$1"/"$element
        if [ -d $dir_or_file ]
        then 
            makeluadir $dir_or_file
        else
            if [[ ${dir_or_file##*.} == "txt" ]]; then  # lua都是txt后缀的， 过滤meta文件
                echo $dir_or_file
                lua2bytes $dir_or_file
            fi
        fi  
    done
}


function lua2bytes(){
    file=$1
    # echo "filename: ${file%.*}"
    bytes=${file%.*}".bytes"
    echo ${luac}
    ${luac} -o $bytes $file
    rm $file
}



function selectplatf(){
    echo "*********  1. 32bit(android) 2. 64bit(ios,osx,pc) ************"
    read  -p "select platform:" pselect
    if [[ $pselect -gt 2 ]]; then #>2 
        echo "warn: not invalid platform selected, reinput again "
        selectplatf        
    fi

    if [[ $pselect -eq 2 ]]; then
        luac=${currdir}"/install-64/bin/luac"
    fi

    cd $luadir
    git clean -df .
    git checkout .
}

selectplatf
makeluadir ${luadir}
echo "job done"