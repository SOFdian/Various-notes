#include "Manager.h"
#include<iostream>

Manager::Manager(int num,string name,int did) {
	this->number = num;
	this->name = name;
	this->did = did;
}
void Manager::showInfo() {
	cout << "职工编号:" << this->number << endl;
	cout << "职工岗位:" << this->getDeptName() << endl;
	cout << "职工姓名" << this->name << endl;
	cout << "职工岗位工作内容:管理" << endl;
	cout << "---------------------------" << endl;
}
string Manager::getDeptName() {
	return "经理";
}