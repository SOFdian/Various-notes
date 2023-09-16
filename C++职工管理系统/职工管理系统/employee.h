#pragma once
#include"worker.h"
#include<iostream>
#include<string>
using namespace std;

class employee :
    public Worker
{
    void showInfo() ;//这里不要打{}，否则会多次声明，打了{}即使里面是空的也是声明过了
    string getDeptName() ;
public://employee不是父类的成员，要使它也为public的话必须单独加访
       //问修饰符，上面两个是父类成员所以在Worker前面加就可以了
    employee(int num, string name, int did);
};

