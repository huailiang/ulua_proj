
# MIT License

# Copyright (c) 2018 huailiang

# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:

# The above copyright notice and this permission notice shall be included in all
# copies or substantial portions of the Software.

# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
# SOFTWARE.


#!/bin/bash

currdir=`cd $(dirname $0); pwd`

luadir=${currdir}"/../Assets/uLua/Resources/Lua/Logic"  

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

    # # reset origin lua file with git 
    # cd $luadir
    # git clean -df .
    # git checkout .
}


selectplatf

makeluadir ${luadir}

echo "*************  job done *************"