#include "employee.h"
#include<iostream>
#include<string>
using namespace std;

employee::employee(int num,string name,int did) {
	this->number = num;
	this->name = name;
	this->did = did;
}
void employee::showInfo() {
	cout << "ְ�����:" << this->number << endl;
	cout << "ְ����λ:" << this->getDeptName() << endl;
	cout << "ְ������" << this->name << endl;
	cout << "ְ����λ��������:����" << endl;
	cout << "---------------------------" << endl;
}
string employee::getDeptName() {
	return "ְ��";
}