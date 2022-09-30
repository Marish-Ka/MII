import numpy as np
import matplotlib.pyplot as plt

print("Введите число K")
K = int(input())
print("Введите четное число N > 1")
N = int(input())
while N % 2 != 0 or N == 1:
    print("Введите четное число N > 1")
    N = int(input())

A = np.random.randint(-10, 10 + 1, N * N).reshape(N, N)
print("A = ")
print(A)

F = A.copy()
print("F = ")
print(F)

center = int(N / 2)
B = A[:center, :center]
C = A[:center, center:]
D = A[center:, :center]
E = A[center:, center:]

print("B = ")
print(B)
print("C = ")
print(C)
print("D = ")
print(D)
print("E = ")
print(E)

minValueC = np.amin(C)
countsMin = np.sum(C[:, :: 2] == minValueC)

maxValueC = np.amax(C)
countsMax = np.sum(C[1::2, :] == maxValueC)

if countsMin < countsMax:
    for i in range(center):
        F[i] = np.hstack((C[i][::-1], B[i][::-1]))
else:
    for j in range(center, len(F)):
        F[:, j] = np.vstack((E, C))[:, j - center]

print("F = ")
print(F)

if np.linalg.det(A) > np.sum(np.diagonal(F)):
    result = np.linalg.inv(A) * A - K * F
else:
    result = (np.transpose(A) + np.transpose(np.tril(A)) - np.linalg.inv(F)) * K

print("result = ")
print(result)
plt.subplot(3, 1, 1)
plt.plot(F)
plt.subplot(3, 1, 2)
plt.imshow(F)
plt.subplot(3, 1, 3)
plt.scatter(F[::, :center], F[::, center:])
plt.show()
