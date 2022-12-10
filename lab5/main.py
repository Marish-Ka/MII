import pandas as pd
from sklearn.metrics import classification_report, accuracy_score
from sklearn.preprocessing import StandardScaler, MinMaxScaler
from sklearn.tree import DecisionTreeClassifier
from sklearn.neighbors import KNeighborsClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.ensemble import GradientBoostingClassifier
from sklearn.model_selection import train_test_split
from matplotlib import pyplot as pl
from matplotlib.colors import ListedColormap


def show(X_train, X_test, y_train, y_test, pred, accuracy, classificator):

# визуализация происход по 3-м главным признакам
# заключается в зрительном сравнении предсказаний классификаторов
# с действительными значениями тестовой выборки, а также в сравнении со значениями
# обучающей  выборки в разных масштабах

    y_train = y_train.map({'Y': 0, 'N': 1})
    y_test = y_test.map({'Y': 0, 'N': 1})
    pred[pred == 'Y'] = 0
    pred[pred == 'N'] = 1

    fig = pl.figure(figsize=(15, 20))
    fig.suptitle("Точность классификатора {} : {}".format(classificator, round(accuracy, 2)))
    colors = ListedColormap(['Purple', 'Yellow'])

    ax = fig.add_subplot(2, 2, 1, projection='3d')
    scatter = ax.scatter3D(X_train[:, 0], X_train[:, 2], X_train[:, 1], c=y_train, cmap=colors)

    ax.set_zlabel("Сумма кредита")
    ax.legend(*scatter.legend_elements(), title="Одобрено/Не одобрено")
    ax.title.set_text("Статус одобрения кредита - обучающая выборка")
    ax.set_xlabel("Доход заявителя")
    ax.set_ylabel("Кредитная история")
    #-------------------------------------------------------------
    ax = fig.add_subplot(2, 2, 2, projection='3d')
    ax.scatter3D(X_train[:, 0], X_train[:, 2], X_train[:, 1], c=y_train, cmap=colors)

    # приближение масштаба, путем ограничения по осям
    ax.set_xlim(0, 15000)
    ax.set_zlim(0, 350)
    ax.set_zlabel("Сумма кредита")
    ax.title.set_text("Статус одобрения кредита - обучающая выборка\n(большинство значений в увеличенном масштабе)")
    ax.set_xlabel("Доход заявителя")
    ax.set_ylabel("Кредитная история")
    # -------------------------------------------------------------

    ax = fig.add_subplot(2, 2, 3, projection='3d')
    ax.scatter3D(X_test[:, 0], X_test[:, 2], X_test[:, 1], c=y_test, marker="x", alpha=0.7, cmap=colors)

    ax.set_xlim(0, 15000)
    ax.set_zlim(0, 350)
    ax.set_zlabel("Сумма кредита")
    ax.title.set_text("Статус одобрения кредита (действительные значения тестовой выборки)")
    ax.set_xlabel("Доход заявителя")
    ax.set_ylabel("Кредитная история")
    #----------------------------------------------------------

    ax = fig.add_subplot(2, 2, 4, projection='3d')
    ax.scatter3D(X_test[:, 0], X_test[:, 2], X_test[:, 1], c=pred, marker="x", alpha=0.7, cmap=colors)

    ax.set_xlim(0, 15000)
    ax.set_zlim(0, 350)
    ax.set_zlabel("Сумма кредита")
    ax.title.set_text("Статус одобрения кредита (значения классификатора)")
    ax.set_xlabel("Доход заявителя")
    ax.set_ylabel("Кредитная история")

    pl.show()

#----------------------------------------------------------
#----------------------------------------------------------
#----------------------------------------------------------

def decision_tree(X_train, X_test, y_train, y_test):

    # обучение модели
    dt = DecisionTreeClassifier()
    dt.fit(X_train, y_train)
    # прогнозирование по тестовому набору
    pred = dt.predict(X_test)

    print("Отчет, показывающий основные метрики классификации методом <Дерево решений>")
    print(classification_report(y_test, pred))

    show(X_train, X_test, y_train, y_test, pred, accuracy_score(y_test, pred), "Дерево решений")

def gradient_boosting(X_train, X_test, y_train, y_test):

    gb = GradientBoostingClassifier()
    gb.fit(X_train, y_train)
    pred = gb.predict(X_test)

    print("Отчет, показывающий основные метрики классификации методом <Повышение градиента>")
    print(classification_report(y_test, pred))

    show(X_train, X_test, y_train, y_test, pred, accuracy_score(y_test, pred), "Повышение градиента")

def random_forest(X_train, X_test, y_train, y_test):

    rf = RandomForestClassifier()
    rf.fit(X_train, y_train)
    pred = rf.predict(X_test)

    print("Отчет, показывающий основные метрики классификации методом <Случайный лес>")
    print(classification_report(y_test, pred, zero_division=0))

    show(X_train, X_test, y_train, y_test, pred, accuracy_score(y_test, pred), "Случайный лес")

def k_neighbors(X_train, X_test, y_train, y_test):

    knn = KNeighborsClassifier()
    knn.fit(X_train, y_train)
    pred = knn.predict(X_test)

    print("Отчет, показывающий основные метрики классификации методом <К ближайших соседей>")
    print(classification_report(y_test, pred))

    show(X_train, X_test, y_train, y_test, pred, accuracy_score(y_test, pred), "К ближайших соседей")

#------------------------------------------------------#
#------------------------------------------------------#
#------------------------------------------------------#

# загрузка данных
train = pd.read_csv("Loan_Data.csv", delimiter=',')
dataset = train.copy()

# ------------------------------------------------------
# ------------------------------------------------------
# выбор набора самых значимых признаков через визуализацию

# преобразование категориальных признаков
dataset = dataset.dropna()
dataset['Gender'] = dataset['Gender'].map({'Male': 0, 'Female': 1})
dataset['Married'] = dataset['Married'].map({'No': 0, 'Yes': 1})
# преобразование признака кол-ва иждевенцев
# (3 и больше человек = "3+" заменено на 10, как относительно большое по смыслу кол-во иждевенцев по сравнению с 0 и 1)
dataset.loc[(dataset.Dependents == '3+'), 'Dependents'] = 5
dataset['Education'] = dataset['Education'].map({'Not Graduate': 0, 'Graduate': 1})
dataset['Self_Employed'] = dataset['Self_Employed'].map({'No': 0, 'Yes': 1})
dataset['Property_Area'] = dataset['Property_Area'].map({'Urban': 0, 'Semiurban': 1, 'Rural': 2})

# явное задание категориального типа
categorys = ['Gender', 'Married', 'Dependents', 'Education', 'Self_Employed', 'Credit_History']
dataset[categorys] = dataset[categorys].astype("category")
label = dataset['Loan_Status']
dataset.drop(['Loan_Status'], axis=1, inplace=True)
dataset.drop(['Loan_ID'], axis=1, inplace=True)

# масштабирование непрерывных функций
continuous_features = set(dataset.columns) - set(categorys)
scaler = MinMaxScaler()
dataset_norm = dataset.copy()
dataset_norm[(list(continuous_features))] = scaler.fit_transform(dataset[list(continuous_features)])

# отбор признаков с помощью случайного леса
clf = RandomForestClassifier()
clf.fit(dataset_norm, label)

pl.figure(figsize=(12, 12))
pl.bar(dataset_norm.columns, clf.feature_importances_)
pl.xticks(rotation=45)
pl.show()

# ------------------------------------------------------
# ------------------------------------------------------

# # удаление малозначимых столбцов
train.drop(['Loan_ID'], axis=1, inplace=True)
train.drop(['Gender'], axis=1, inplace=True)
train.drop(['Married'], axis=1, inplace=True)
train.drop(['Dependents'], axis=1, inplace=True)
train.drop(['Education'], axis=1, inplace=True)
train.drop(['Self_Employed'], axis=1, inplace=True)
train.drop(['CoapplicantIncome'], axis=1, inplace=True)
train.drop(['Loan_Amount_Term'], axis=1, inplace=True)
train.drop(['Property_Area'], axis=1, inplace=True)

# вывод датасета и статистика по нему
print("Размерность датасета : ", train.shape)
print(train)
print("Описательная статистика по датасету:\n", train.describe(include="all"))
print("Кол-во пропусков изначально:\n", train.isna().sum())
#-----------------------------------------------------------

# очистка данных

# внесение вместо недостающих значений признака <сумма кредита> медиану
train['LoanAmount'] = train['LoanAmount'].fillna(train['LoanAmount'].median())
# удаление дубликатов и строк с пропущенными значениями
train = train.dropna()
train = train.drop_duplicates()
# явное изменене на категориальный тип
train['Credit_History'] = train['Credit_History'].astype("category")

#---------------------------------------------------------------

# разделение на набор для обучения и тестирования
y = train.Loan_Status
train.drop(['Loan_Status'], axis=1, inplace=True)
X = train.iloc[::].values
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.25, random_state=2)

# стандартизация данных
scaler = StandardScaler()
scaler.fit_transform(X_train)
scaler.fit_transform(X_test)

#---------------------------------------------------------------

# запуск работы классификаторов и их визуализация
decision_tree(X_train, X_test, y_train, y_test)
gradient_boosting(X_train, X_test, y_train, y_test)
random_forest(X_train, X_test, y_train, y_test)
k_neighbors(X_train, X_test, y_train, y_test)
