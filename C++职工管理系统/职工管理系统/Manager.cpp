#include "Manager.h"
#include<iostream>

Manager::Manager(int num,string name,int did) {
	this->number = num;
	this->name = name;
	this->did = did;
}
void Manager::showInfo() {
	cout << "ְ�����:" << this->number << endl;
	cout << "ְ����λ:" << this->getDeptName() << endl;
	cout << "ְ������" << this->name << endl;
	cout << "ְ����λ��������:����" << endl;
	cout << "---------------------------" << endl;
}
string Manager::getDeptName() {
	return "����";
}