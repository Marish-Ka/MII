import math
import random
from math import sqrt
import csv
from matplotlib import pyplot as pl
from sklearn.neighbors import KNeighborsClassifier
from sklearn import metrics
from sklearn.model_selection import train_test_split


############################################
#   Генерация данных с 3 разными классами  #
# Класс 0 - фрукты, 1 - овощи, 2 - протеин #
############################################
fieldnames = ["продукт", "сладость", "хруст", "класс"]
data = [
    ['яблоко', 7, 7, 0],
    ['салат', 2, 5, 1],
    ['бекон', 1, 2, 2],
    ['банан', 9, 1, 0],
    ['орехи', 1, 5, 2],
    ['рыба', 1, 1, 2],
    ['сыр', 1, 1, 2],
    ['виноград', 8, 1, 0],
    ['морковь', 2, 8, 1],
    ['апельсин', 6, 1, 0],
    ######################
    #подгтовленные дополнительные примеры
    ['ананас', 7, 2, 0],
    ['гранат', 6, 3, 0],
    ['яйца', 1, 2, 2],
    ['капуста', 2, 9, 1],
    ['красная икра', 1, 3, 2],
    ['редис', 3, 10, 1],
    ['кабочок', 2, 6, 1],
    ['персик', 7, 4, 0],
    ['творог', 2, 2, 2],
    ['тыква', 4, 6, 1]]

file = 'dataset1.csv'
with open(file, 'w', newline='', encoding='utf-16') as w_f:
    writer = csv.writer(w_f)
    writer.writerow(fieldnames)
    writer.writerows(data)


#########################################
# Генерация данных с 4 разными классами #
#      Класс 0 - фрукты, 1 - овощи,     #
#       2 - протеин, 3 - сладости       #
#########################################

data_two = [
    ['яблоко', 7, 7, 0],
    ['салат', 2, 5, 1],
    ['бекон', 1, 2, 2],
    ['пахлава', 10, 7, 3],
    ['банан', 9, 1, 0],
    ['орехи', 1, 5, 2],
    ['помадная конфета', 10, 1, 3],
    ['рыба', 1, 1, 2],
    ['сыр', 1, 1, 2],
    ['виноград', 8, 1, 0],
    ['чурчхела', 9, 3, 3],
    ['капуста', 2, 9, 1],
    ['морковь', 2, 8, 1],
    ['козинаки', 9, 10, 3],
    ['апельсин', 6, 1, 0],
    ['вафли', 8, 9, 3],
    ['гранат', 6, 3, 0],
    ['яйца', 1, 2, 2],
    ['красная икра', 1, 3, 2],
    ['редис', 3, 10, 1],
    ['кабочок', 2, 6, 1],
    ['чакчак', 10, 8, 3],
    ['персик', 7, 4, 0],
    ['творог', 2, 2, 2],
    ['тыква', 4, 6, 1]]

file_two = 'dataset2.csv'
with open(file_two, 'w', newline='', encoding='utf-16') as w_f:
    writer = csv.writer(w_f)
    writer.writerow(fieldnames)
    writer.writerows(data_two)

##############################################
# Чтение в первый датасет данных с 3 классами#
##############################################
dataset=[]
with open(file, 'r', encoding='utf-16') as r_f:
    reader = csv.DictReader(r_f, delimiter=",")
    dataset=[[int(row['сладость']), int(row['хруст']), int(row['класс'])] for row in reader]

###############################################
# Чтение во второй датасет данных с 4 классами#
###############################################
dataset_two=[]
with open(file_two, 'r', encoding='utf-16') as r_f:
    reader = csv.DictReader(r_f, delimiter=",")
    dataset_two=[[int(row['сладость']), int(row['хруст']), int(row['класс'])] for row in reader]


############################################
# метрический классификатор по методу k-NN #
############################################
def knn_classifieer(data, test_size, k):

    # определение размера обучающей и тестовой выборки
    train_data = data[0:int(len(data)*(1-test_size))]
    test_data = data[int(len(data)*(1-test_size)):]

    # ф-я определения расстояния между 2 точками
    def dist(a, b):
        return math.sqrt((a[0] - b[0]) ** 2 + (a[1] - b[1]) ** 2)

    result_classification = []
    count_classes = len(set([train_data[i][2] for i in range(len(train_data))]))

    for test_item in test_data:
        #заполнение списка значениями расстояний от тестовой точки до каждой обучающей точки и значением до какой точки расстояние в 1 аргументе
        test_dist = [[dist(test_item[0:2], train_data[i][0:2]), train_data[i][2]] for i in range(len(train_data))]
        weight_of_class = [0] * count_classes
        # для К ближайших соседей
        for near_neighbor in sorted(test_dist)[0:k + 1]:
            # считается, сколько из К ближ. соседей относится к каждому классу
            weight_of_class[near_neighbor[1]] += 1
        # в результат заносится номер класса, к которому принадлежат большинство из К ближ. соседей
        result_classification.append(next(number_class for number_class in range(count_classes)
                                                    if weight_of_class[number_class] == max(weight_of_class)))

    # определяется точность, путем суммирования кол-ва правильно определенных класов, разделенных на кол-во тестовой выборки
    classifier_accuracy = sum([int(result_classification[i] == test_data[i][2])
                               for i in range(len(test_data))]) / (len(test_data))
    print("Точность метрического классификатора, когда тестовая выборка составляет ", test_size*100, "% данных при k=", k, ": ", str(classifier_accuracy))

    return result_classification


#################################
# метод k-NN библиотеки sklearn #
#################################
def knn_sclearn(data, test_size, k):
    target = list(data[i][2] for i in range(len(data)))
    X_train, X_test, y_train, y_test = train_test_split(data, target, test_size=test_size, shuffle=False, stratify=None)

    knn_classifier = KNeighborsClassifier(k)
    knn_classifier.fit(X_train, y_train)
    result_classification = knn_classifier.predict(X_test)
    print("Точность классификатора sclearn, когда тестовая выборка составляет ", test_size*100, "% данных при k=", k, ": ", metrics.accuracy_score(y_test, result_classification))
    return result_classification

############################
# визуализации результатов #
############################
def showData(type_classifier, data, test_size, k):

    train_data = data[0:int(len(data) * (1 - test_size))]
    test_data = data[int(len(data) * (1 - test_size)):]

    result_class = type_classifier(data, test_size, k)

    fig, ax = pl.subplots(figsize=(6.5, 5))
    scatter = ax.scatter([train_data[i][0] for i in range(len(train_data))],
                         [train_data[i][1] for i in range(len(train_data))],
                         c=[train_data[i][2] for i in range(len(train_data))],
                         marker="o",
                         alpha=0.5)

    ax.scatter([test_data[i][0] for i in range(len(test_data))],
                         [test_data[i][1] for i in range(len(test_data))],
                         c=result_class,
                         marker="x",
                         alpha=0.5)

    legend = ax.legend(*scatter.legend_elements(), title="Классы")
    ax.add_artist(legend)
    ax.title.set_text("Классификация продуктов")
    ax.set_xlabel("Сладость")
    ax.set_ylabel("Хруст")
    ax.text(7, 11.2, "o - обучающая выборка\nх - тестовая выборка")
    pl.show()

#######################################################3

showData(knn_sclearn, dataset, 0.4, 5)
showData(knn_classifieer, dataset, 0.2, 3)
showData(knn_sclearn, dataset_two, 0.2, 3)