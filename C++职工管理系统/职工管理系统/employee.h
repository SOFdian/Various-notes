#pragma once
#include"worker.h"
#include<iostream>
#include<string>
using namespace std;

class employee :
    public Worker
{
    void showInfo() ;//���ﲻҪ��{}�������������������{}��ʹ�����ǿյ�Ҳ����������
    string getDeptName() ;
public://employee���Ǹ���ĳ�Ա��Ҫʹ��ҲΪpublic�Ļ����뵥���ӷ�
       //�����η������������Ǹ����Ա������Workerǰ��ӾͿ�����
    employee(int num, string name, int did);
};

