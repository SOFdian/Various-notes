#include "Boss.h"
#include<iostream>

Boss::Boss(int num, string name, int did) {
	this->number = num;
	this->name = name;
	this->did = did;
}

void Boss::showInfo() {
	cout << "职工编号:" << this->number << endl;
	cout << "职工岗位:" << this->getDeptName() << endl;
	cout << "职工姓名" << this->name << endl;
	cout << "职工岗位工作内容:领导" << endl;
	cout << "---------------------------" << endl;
}
string Boss::getDeptName() {
	return "老板";
}