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
	cout << "职工编号:" << this->number << endl;
	cout << "职工岗位:" << this->getDeptName() << endl;
	cout << "职工姓名" << this->name << endl;
	cout << "职工岗位工作内容:摸鱼" << endl;
	cout << "---------------------------" << endl;
}
string employee::getDeptName() {
	return "职工";
}