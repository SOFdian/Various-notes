#include<iostream>
#include<string>
#include"workerManager.h"
#include"worker.h"
#include"employee.h"
#include"Manager.h"
#include"Boss.h"
using namespace std;

WorkManager::WorkManager() {
	//1.无初始文件
	ifstream ifs;
	ifs.open(FILE, ios::in);
	if (!ifs.is_open()) {
		this->m_num = 0;//0人
		this->m_EmpArray = NULL;
		this->isEmpty = true;
		cout << "警告：无初始文件！！！！" << endl;
		ifs.close();
		return;//跳出
	}
	//2.初始文件为空
	char ch;
	ifs >> ch;//读第一个字符，看第一个字符是不是结尾字符
	if (ifs.eof()) {
		this->m_num = 0;//0人
		this->m_EmpArray = NULL;
		this->isEmpty = true;
		cout << "警告：初始文件为空！！！！" << endl;
		ifs.close();
		return;//跳出
	}
	//3.初始文件不为空
	//这里写在单独的函数可能比较好，否则可能会接着上面的步骤读取数据？
	init_Emp();
}
//构造函数初始化
WorkManager::~WorkManager() {
	if (this->m_EmpArray != NULL) {
		delete[] this->m_EmpArray;
		this->m_EmpArray = NULL;
	}
}
//析构
void WorkManager::showMenu() {
	cout << "************************************" << endl;
	cout << "****** 欢迎使用职工管理系统！*******" << endl;
	cout << "***********0.退出管理系统***********" << endl;
	cout << "***********1.增加职工信息***********" << endl;
	cout << "***********2.显示职工信息***********" << endl;
	cout << "***********3.删除离职职工***********" << endl;
	cout << "***********4.修改职工信息***********" << endl;
	cout << "***********5.查找职工信息***********" << endl;
	cout << "***********6.按照编号排序***********" << endl;
	cout << "***********7.清空所有文档***********" << endl;
	cout << "************************************" << endl;
	cout << endl;
}
//菜单栏
int WorkManager::Exit() {
	cout << "欢迎下次使用" << endl;
	return 0;
}
//退出
void WorkManager::addWorker() {
	int num=0,job;
	string name;
	Worker* worker = NULL;
	while (1) {
		cout << "员工编号处输入-1，即可退出员工增添系统" << endl;
		cout << "请输入员工的编号（输入非数字会导致程序崩溃）" << endl;
		cin >> num;//如果这里输入了非整形会导致输入内容读不进num，num仍然为0，但是输入的非整形内容不会消失，之后会直接跳过这一行（因为一直有一个输入没有读），于是num始终为0且无法更改，无限循环
		if (num == -1) {
			system("cls");
			this->showMenu();//再次显示菜单
			break;
		}else if (num < 0) {
			cout << "请输入大于等于0的编号！" << endl;
		}else if (isExist(num)) {
			cout << "该编号的员工已存在！" << endl;
		}		//三个限制条件↑↑↑
		//以下才是有效输入，无效输入会重新进入输入编号阶段
		else {
			cout << "请输入员工的姓名" << endl;
			cin >> name;
			cout << "请输入员工的职务" << endl;
			cout << "1.职工" << endl;
			cout << "2.经理" << endl;
			cout << "3.老板" << endl;
			cin >> job;
			switch (job)
			{
			case 1:
				worker = new employee(num, name, job);
				break;
			case 2:
				worker = new Manager(num, name, job);
				break;
			case 3:
				worker = new Boss(num, name, job);
				break;
			default:
				cout << "输入不合法！" << endl;
				break;
			}
			if (job == 1 || job == 2 || job == 3) {
				//判断员工类型
				int newsize = this->m_num + 1;//增加一名员工后的员工数量
				Worker** newspace = new Worker * [newsize];//这里各种类型的关系不是很懂
				/*
				* new了一个储存着newsize个类型为Worker*的数据的空数组（记作a），返回了一个指向该数组的指针
				* 同时newspace是一个指向指针的指针
				* 由于指针和数组的特殊联系
				* newspace[i]即a[i],是一个指向特定Worker的指针
				*/
				//现在需要将类型为Worker*的数据传入newspace
				newspace[newsize - 1] = worker;//在这里因为里面填了newsize而不是newsize-1导致我浪费了1小时，回头再看看
				//worker是一个指向Worker的某个子类的指针，类型就是Worker*，将其传入newspace
				if (this->m_EmpArray != NULL) {
					for (int i = 0; i < this->m_num; i++) {
						newspace[i] = this->m_EmpArray[i];
						//将原有数据依次传递
						//p1[n1]等价于*(p1 + n1)
					}
				}
				delete[] this->m_EmpArray;//释放数组时加上【】
				//释放原来的空间
				this->m_EmpArray = newspace;
				//指向现在的空间
				this->m_num++;
				cout << "添加成功！" << endl;
				this->isEmpty = false;
				this->save();//保存
				this->showWorker();
		}
		}
		
	}
	
}
//添加员工(有一个无限循环的bug未排除也未找到触发条件)
void WorkManager::showWorker() {
	for (int i = 0; i < m_num; i++)
	{
		//利用多态调用接口
		this->m_EmpArray[i]->showInfo();
	}
	
}
//展示员工
void WorkManager::changeWorker(){
	int id;
	cout << "请输入想要修改信息的员工编号" << endl;
	cin >> id;
	if (isExist(id)) {
		int i = this->findPosition(id);
		int num, job;
		string name;
		cout << "请输入新的员工编号" << endl;
		cin >> num;
		cout << "请输入员工的姓名" << endl;
		cin >> name;
		cout << "请输入员工的职务" << endl;
		cout << "1.职工" << endl;
		cout << "2.经理" << endl;
		cout << "3.老板" << endl;
		cin >> job;
		switch (job)
		{
		case 1:
			delete m_EmpArray[i];//原来的删掉
			m_EmpArray[i] = new employee(num, name, job);//指向新建的
			this->save();//保存
			break;
		case 2:
			delete m_EmpArray[i];
			m_EmpArray[i] = new Manager(num, name, job);
			this->save();
			break;
		case 3:
			delete m_EmpArray[i];
			m_EmpArray[i] = new Boss(num, name, job);
			this->save();
			break;
		default:
			cout << "输入不合法！" << endl;
			break;
		}
	}
	else {
		cout << "查无此人！" << endl;
	}
}
//改变员工
void WorkManager::deleteAll(){
	delete[] m_EmpArray;
	m_EmpArray = NULL;
	m_num = 0;
	isEmpty = true;
	this->save();
}
//清空所有
void WorkManager::deleteWorker() {
	int id;
	cout << "请输入要删除员工的编号" << endl;
	cin >> id;
	if (isExist(id)) {
		if (m_num - 1 == 0) {
			delete[] this->m_EmpArray;//如果该员工是唯一一个员工就直接删掉数组并且置空
			this->m_EmpArray = NULL;
			this->isEmpty = true;
			this->m_num = 0;
		}else {
			Worker** newspace = new Worker * [m_num - 1];
			this->m_num--;
			for (int i = 0, j = 0; i < m_num; j++) {
				if (id != m_EmpArray[j]->number) {
					newspace[i] = m_EmpArray[j];//遍历原数组（排除删除员工）存到新数组里
					i++;
				}
			}
			delete[] this->m_EmpArray;
			this->m_EmpArray = newspace;
		}
		cout << "操作成功！现存员工" << m_num << "人！" << endl;
		this->save();
	}
	else {
		cout << "查无此人！" << endl;
	}


}
//删除员工
void WorkManager::findWorker(){
	int id;
	cout << "请输入要查找的员工编号：" << endl;
	cin >> id;
	if (!isExist(id)) {
		cout << "查无此人！" << endl;
	}
	else {
		m_EmpArray[findPosition(id)]->showInfo();
	}
}
//查找员工
void WorkManager::sortWorker(){
	cout << "请选择排列方式" << endl;
	cout << "1.按编号升序" << endl;
	cout << "2.按编号降序" << endl;
	int num;
	int MinOrMax;
	cin >> num;
	for (int i = 0; i < m_num-1; i++) {
		MinOrMax = i;
		for (int j = i + 1; j < m_num; j++) {
			if (num == 1) {//升序
				if (m_EmpArray[j]->number < m_EmpArray[i]->number) {//将数组中第i个number与后面的number依次比较
					MinOrMax = j;//发现后面的第j个number比第i个number小就交换
				}
			} 
			if (num == 2) {
				if (m_EmpArray[j]->number > m_EmpArray[i]->number) {//将数组中第i个number与后面的number依次比较
					MinOrMax = j;
				}
			}
			if (MinOrMax != i) {
				Worker* exchange;
				exchange = m_EmpArray[j];
				m_EmpArray[j] = m_EmpArray[i];
				m_EmpArray[i] = exchange;
				MinOrMax = i;//交换完之后第i个number又是最小的了；
			}
		}
		cout << "交换成功！" << endl;
		this->save();
		this->showWorker();
	}
}
//排序
void WorkManager::save() {
	ofstream ofs;
	ofs.open(FILE, ios::out);
	if (isEmpty == false) {//非空时
		for (int i = 0; i < this->m_num; i++) {
			ofs << this->m_EmpArray[i]->number << " "
				<< this->m_EmpArray[i]->name << " "
				<< this->m_EmpArray[i]->did << endl;
		}
	}
	else {
		ofs << "";
	}
	ofs.close();
	cout << "已保存！" << endl;
}
//文件写入
void WorkManager::getNum() {
	ifstream ifs;
	ifs.open(FILE, ios::in);

	int id, did;
	string name;
	while (ifs >> id && ifs >> name && ifs >> did) {
		this->m_num++;
	}
}
//计算文件内人数
void WorkManager::init_Emp() {
	ifstream ifs;
	ifs.open(FILE, ios::in);
	int id, did;
	string name;
	getNum();
	//计算员工个数
	m_EmpArray = new Worker * [m_num];//创建对应个数的数组
	int i = 0;//while循环用
	while (ifs >> id && ifs >> name && ifs >> did) {
		if (did == 1) {
			m_EmpArray[i] = new employee(id, name, did);
		}
		else if (did == 2) {
			m_EmpArray[i] = new Manager(id, name, did);
		}
		else if (did == 3) {
			m_EmpArray[i] = new Boss(id, name, did);
		}
		i++;
	}
}
//将文件内原有员工读入m_EmpArray
int WorkManager::findPosition(int id) {
	for (int i = 0; i < m_num; i++) {
		if (id == m_EmpArray[i]->number) {
			return i;
			//前面判断id一定存在，一定会return一个i
		}
	}
}
//前面要判断id是否存在
bool WorkManager::isExist(int id) {
	for (int i = 0; i < m_num; i++) {
		if (id == this->m_EmpArray[i]->number) {
			return true;//遍历看是否有该id
		}
	}
	return false;//遍历完都没有就return false
}
