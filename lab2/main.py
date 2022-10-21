import numpy as np

import matplotlib.pyplot as plt


K = int(input("Введите число K "))
N = int(input("Введите четное число N > 1 "))
while N % 2 != 0 or N == 1:
    N = int(input("Введите четное число N > 1 "))

A = np.random.randint(-10, 10 + 1, N * N).reshape(N, N)
print(f'\nA = \n{A}\n')

F = A.copy()
print(f'\nF = \n{F}\n')

center = int(N / 2)
B = A[:center, :center]
C = A[:center, center:]
D = A[center:, :center]
E = A[center:, center:]

print(f'\nB = \n{B}\n')
print(f'\nC = \n{C}\n')
print(f'\nD = \n{D}\n')
print(f'\nE = \n{E}\n')

minValueC = np.amin(C)
countsMin = np.sum(C[:, :: 2] == minValueC)

maxValueC = np.amax(C)
countsMax = np.sum(C[1::2, :] == maxValueC)

if countsMin < countsMax:
    print("Кол-во мин. чисел в нечетных столбцах в матрице С меньше,чем макс. чисел в четных строках")
    for i in range(center):
        F[i] = np.hstack((C[i][::-1], B[i][::-1]))
else:
    print("Кол-во мин. чисел в нечетных столбцах в матрице С больше,чем макс. чисел в четных строках")
    for j in range(center, len(F)):
        F[:, j] = np.vstack((E, C))[:, j - center]

print("Матрица F после изменения")
print(f'F = \n{F}\n')

det_A = np.linalg.det(A)
print(f'Определитель матрицы А: = {det_A}')

diagonals_A = np.sum(np.diagonal(F)) + sum(np.diagonal(np.flip(F, axis=1)))
print(f'Сумма диагоналей матрицы F: = {diagonals_A}')

if det_A > diagonals_A:
    print("Определитель матрицы А больше суммы диагоналей матрицы F")
    result = np.linalg.inv(A) * A - K * F
else:
    print("Определитель матрицы А меньше суммы диагоналей матрицы F")
    result = (np.transpose(A) + np.transpose(np.tril(A)) - np.linalg.inv(F)) * K

print(f'Результат = {result}')


fig = plt.figure(figsize=(8, 8))
fig.suptitle('Demonstration Matplotlib', fontsize = 30)

fig1 = fig.add_subplot(411)
fig1.plot(F)
fig1.set_title("Demonstration plot", fontsize = 14)

fig2 = fig.add_subplot(423)
fig2.imshow(F, cmap='plasma', aspect='equal', interpolation='gaussian', origin="lower")
fig2.set_title("Demonstration imshow", fontsize = 14)

fig3 = fig.add_subplot(424)
fig3.pcolormesh(F, cmap='plasma', edgecolors="k", shading='flat')
fig3.set_title("Demonstration pcolormesh", fontsize = 14)

fig4 = fig.add_subplot(413)
fig4.scatter(F[::, :center], F[::, center:])
fig4.set_title("Demonstration scatter", fontsize = 14)

fig5 = fig.add_subplot(414)
fig5.hist(F)
fig5.set_title("Demonstration hist", fontsize = 14)

plt.subplots_adjust(hspace=0.5)
plt.show()
