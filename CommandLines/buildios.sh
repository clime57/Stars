# #!/bin/bash  

#参数判断  
if [ $# != 3 ];then
    echo "Params error!"  
    echo "Need three params: 1.path of project 2.name of ipa file 3.version of app"
    exit  
elif [ ! -d $1 ];then  
    echo "The first param is not a dictionary."  
    exit      

fi  
#工程路径  
xcode_project_path=$1

#app名称
app_name=$2
#ipa name
ipa_name=$3
#build文件夹路径  
build_path=${xcode_project_path}/build/Release-iphoneos
ipa_path=${WORKSPACE}
#清理#
#xcodebuild  clean

#编译工程  
cd $xcode_project_path
xcodebuild || exit

#打包 下面代码我是新加的#  
xcrun -sdk iphoneos PackageApplication -v ${build_path}/${app_name}.app -o ${ipa_path}/${ipa_name}.ipa
