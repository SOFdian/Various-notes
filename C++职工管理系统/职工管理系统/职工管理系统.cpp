#include<iostream>
#include"workerManager.h"
#include"worker.h"
#include"employee.h"
#include"Manager.h"
#include"Boss.h"
using namespace std;


//void test1() {
//	Worker* w1 = new employee(1, "����", 1);
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
//}//��̬����
int main() {
	WorkManager w1;//�������󡰹����ߡ�
	w1.showMenu();//�������˵���ʾ����
	int num = -1;
	while (1) {
		cout << "����������ѡ��" << endl;
		cin >> num;
		if (w1.isEmpty == true&& num>1) {
			cout<<"�ļ������ڻ�Ϊ�գ�����¼��Ա����Ϣ��"<< endl;
		}
		else {
			switch (num)
			{
			case 0:
				return w1.Exit();
			case 1:
				w1.addWorker();
				/*w1.save();*/
				//��ӳɹ��ű���
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
				cout << "���벻�Ϸ�" << endl;
				break;
			}
		}
		
		
	}
	system("pause");

	return 0;
}