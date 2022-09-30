С клавиатуры вводится два числа K и N. Квадратная матрица А(N,N), состоящая из 4-х равных по размерам подматриц, B,C,D,E заполняется случайным образом целыми числами 
в интервале [-10,10]. 
По сформированной матрице F (или ее частям) необходимо вывести не менее 3 разных графиков.
Допускается использование библиотек numpy  и mathplotlib

10.	Формируется матрица F следующим образом: скопировать в нее А и если в С количество минимальных чисел в нечетных столбцах меньше, чем количество максимальных чисел в четных строках, то поменять местами В и С симметрично, иначе С и Е поменять местами несимметрично. При этом матрица А не меняется. После чего если определитель матрицы А больше суммы диагональных элементов матрицы F, то вычисляется выражение: A-1*A – K * F, иначе вычисляется выражение (AТ +GТ-F-1)*K, где G-нижняя треугольная матрица, полученная из А. Выводятся по мере формирования А, F и все матричные операции последовательно.