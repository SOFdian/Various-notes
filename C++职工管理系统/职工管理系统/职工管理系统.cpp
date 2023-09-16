#include<iostream>
#include"workerManager.h"
#include"worker.h"
#include"employee.h"
#include"Manager.h"
#include"Boss.h"
using namespace std;


//void test1() {
//	Worker* w1 = new employee(1, "张三", 1);
//	w1->showInfo();
//	delete w1;
//
//	w1 = new Manager(2, "zlkd", 5);
//	w1->showInfo();
//	delete w1;
//
//	w1 = new Boss(3, "skadjf", 4);
//	w1->showInfo();
//	delete w1;
//
//}//多态测试
int main() {
	WorkManager w1;//创建对象“管理者”
	w1.showMenu();//调用主菜单显示函数
	int num = -1;
	while (1) {
		cout << "请输入宁的选择：" << endl;
		cin >> num;
		if (w1.isEmpty == true&& num>1) {
			cout<<"文件不存在或为空！请先录入员工信息！"<< endl;
		}
		else {
			switch (num)
			{
			case 0:
				return w1.Exit();
			case 1:
				w1.addWorker();
				/*w1.save();*/
				//添加成功才保存
				break;
			case 2:
				w1.showWorker();
				break;
			case 3:
				w1.deleteWorker();
				/*w1.save();*/
				break;
			case 4:
				w1.changeWorker();
				break;
			case 5:
				w1.findWorker();
				break;
			case 6:
				w1.sortWorker();
				break;
			case 7:
				w1.deleteAll();
				break;
			case 8:
				system("cls");
				w1.showMenu();
				break;
			default:
				cout << "输入不合法" << endl;
				break;
			}
		}
		
		
	}
	system("pause");

	return 0;
}