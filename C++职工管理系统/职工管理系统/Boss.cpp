#include "Boss.h"
#include<iostream>

Boss::Boss(int num, string name, int did) {
	this->number = num;
	this->name = name;
	this->did = did;
}

void Boss::showInfo() {
	cout << "ְ�����:" << this->number << endl;
	cout << "ְ����λ:" << this->getDeptName() << endl;
	cout << "ְ������" << this->name << endl;
	cout << "ְ����λ��������:�쵼" << endl;
	cout << "---------------------------" << endl;
}
string Boss::getDeptName() {
	return "�ϰ�";
}