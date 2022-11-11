import random as rand
from russian_names import RussianNames
import csv

count_str = rand.randint(1000, 2000)
pers_number = list(range(1, count_str + 1))

count_men = int(rand.randint(1, count_str))
count_women = int(count_str - count_men)

name_m = RussianNames(count=count_men, gender=1.0, patronymic=True, name_reduction=True, patronymic_reduction=True)
name_w = RussianNames(count=count_women, gender=0, patronymic=True, name_reduction=True, patronymic_reduction=True)
names = name_m.get_batch() + name_w.get_batch()

sex = ['Мужчина'] * count_men + ['Женщина'] * count_women

year_of_birth = list(rand.randint(1955, 2004) for _ in range(count_str))

start_year = list(rand.randint(year + 18, 2022) for year in year_of_birth)

structure_of_company = {
    'Отдел администрирования': ['Администратор офиса', 'Системный администратор', 'Водитель', 'Техник'],
    'Отдел коммерции': ['Менеджер по продажам', 'Администратор по учету продаж', 'Cпециалист по рекламе',
                        'Дизайнер-верстальщик', 'Сервис-менеджер'],
    'Отдел по работе с клиентами': ['Специалист по работе с клиентами', 'Специалист по сбору инф-ции',
                                    'ГИС- специалист'],
    'Отдел бухгалтерского учета': ['Бухгалтер', 'Старший бухгалтер'],
    'Отдел по работе с общественностью': ['PR-менеджер', 'Коммьюнити-менеджер']}
department = list(rand.choice(list(structure_of_company.keys())) for _ in range(count_str))
post = list(rand.choice(structure_of_company.get(department_item)) for department_item in department)

salary = list(rand.randint(20000, 50000) for _ in range(count_str))

count_completed_projects = list(rand.randint(5, 25) for _ in range(count_str))

field_names = ['Табельный номер', 'И.О. Фамилия', 'Пол', 'Год рождения', 'Год начала работы в компании',
               'Подразделение', 'Должность', 'Оклад', 'Кол-во выполненных проектов']

data = list(zip(pers_number, names, sex, year_of_birth, start_year, department, post, salary, count_completed_projects))
rand.shuffle(data)

file = 'example.csv'
with open(file, 'w', newline='', encoding='utf-16') as w_f:
    writer = csv.writer(w_f)
    writer.writerow(field_names)
    writer.writerows(data)

################################
import numpy as np

with open(file, 'r', encoding='utf-16') as r_f:
    reader = csv.reader(r_f, delimiter=",")
    full_data_in_file = list(reader)

    headers = full_data_in_file[0]
    number_of_column = list(range(0, len(headers)))
    dict_header = dict.fromkeys(headers) | dict(zip(headers, number_of_column))

    data_of_personal = full_data_in_file[1:]

    count_employees = len(data_of_personal)

    def getting_column(name, types):
        return list(types(item[dict_header.get(name)]) for item in data_of_personal)

    count_m = np.sum(np.array(getting_column('Пол', str)) == 'Мужчина')
    count_w = np.sum(np.array(getting_column('Пол', str)) == 'Женщина')

    birth_oldest_employee = np.min(getting_column('Год рождения', int))
    birth_younger_employee = np.max(getting_column('Год рождения', int))

    mean_salary = np.mean(getting_column('Оклад', int))
    all_salary = np.sum(getting_column('Оклад', int))

    median_of_count_proj = np.median(getting_column('Кол-во выполненных проектов', int))
    std_of_count_proj = np.std(getting_column('Кол-во выполненных проектов', int))

    departments = getting_column('Подразделение', str)
    popular_departament = max(set(departments), key=lambda x: departments.count(x))
    posts = getting_column('Должность', str)
    popular_posts = max(set(posts), key=lambda x: posts.count(x))

    print("Статистические данные по компании: ")
    print("Кол-во сторудникков в компании - " + str(count_employees))
    print("Кол-во мужчин - " + str(count_m))
    print("Кол-во женщин - " + str(count_w))
    print("Год рождения самых старших сотрудников - " + str(birth_oldest_employee))
    print("Год рождения самых младших сотрудников - " + str(birth_younger_employee))
    print("Средняя зарплата в компании - " + str(mean_salary))
    print("Общая сумма зарплаты в компании - " + str(all_salary))
    print("Медиана кол-ва выполненных проектов по сотрудникам - " + str(median_of_count_proj))
    print("Стандартное отклонение кол-ва выполненных проектов - " + str(std_of_count_proj))
    print("Больше всего людей работает в подразделении - " + popular_departament)
    print("Одна из популярных должностей - " + popular_posts + '\n')

############################################
import pandas as pd

pd.set_option('display.max_rows', None)
pd.set_option('display.max_columns', None)
pd.set_option('display.max_colwidth', None)

data_df = pd.read_csv(file, sep=',', encoding='utf-16  ')

count_employees_df = len(data_df.index)

count_m_df = len(data_df[data_df['Пол'] == 'Мужчина'])
count_w_df = len(data_df[data_df['Пол'] == 'Женщина'])

birth_oldest_employee_df = data_df['Год рождения'].min()
birth_younger_employee_df = data_df['Год рождения'].max()

mean_salary_df = data_df['Оклад'].mean()
all_salary_df = data_df['Оклад'].sum()

median_of_count_proj_df = data_df['Кол-во выполненных проектов'].median()
std_of_count_proj_df = data_df['Кол-во выполненных проектов'].std()

popular_departament_df = list(data_df['Подразделение'].value_counts().loc[data_df['Подразделение'].value_counts() == data_df['Подразделение'].value_counts().max()].index)
popular_posts_df = list(data_df['Должность'].value_counts().loc[data_df['Должность'].value_counts() == data_df['Должность'].value_counts().max()].index)

print("Статистические данные по компании через dataframe: ")
print("Кол-во сторудникков в компании - " + str(count_employees_df))
print("Кол-во мужчин - " + str(count_m_df))
print("Кол-во женщин - " + str(count_w_df))
print("Год рождения самых старших сотрудников - " + str(birth_oldest_employee_df))
print("Год рождения самых младших сотрудников - " + str(birth_younger_employee_df))
print("Средняя зарплата в компании - " + str(mean_salary_df))
print("Общая сумма зарплаты в компании - " + str(all_salary_df))
print("Медиана кол-ва выполненных проектов по сотрудникам - " + str(median_of_count_proj_df))
print("Стандартное отклонение кол-ва выполненных проектов - " + str(std_of_count_proj_df))
print("Больше всего людей работает в подразделениях - " + ", ".join(popular_departament_df))
print("Популярные должности - " + ", ".join(popular_posts_df))

##################

import matplotlib.pyplot as plt

fig1, ax1 = plt.subplots(figsize=(10, 6))
ax1.scatter(x=data_df['Год рождения'], y=data_df['Оклад'])
plt.xlabel("Год рождения")
plt.ylabel("Оклад")
plt.title("Зависимость оклада от возраста сотрудника")
plt.show()

salary_department_df = {}
departments_df = data_df['Подразделение'].unique()
for item in departments_df:
    department = data_df[data_df['Подразделение'] == item]
    salary_department_df[item] = department['Оклад'].sum()
value_pie = list(salary_department_df[item] for item in departments_df)
fig2, ax2 = plt.subplots(figsize=(10, 6))
ax2.pie(value_pie, labels=departments_df, autopct='%.2f%%')
plt.title("Доли от фонда заработной платы по подразделениям в %")
plt.show()

count_women_in_post = {}
count_men_in_post ={}
post_df = data_df['Должность'].unique()
for item in post_df:
    post = data_df[data_df['Должность'] == item]
    count_women_in_post[item] = sum(post['Пол'] == 'Женщина')
    count_men_in_post[item] = sum(post['Пол'] == 'Мужчина')
value_men = list(count_men_in_post[item] for item in post_df)
value_women = list(count_women_in_post[item] for item in post_df)
fig3, ax3 = plt.subplots(figsize=(10, 6))
ax3.barh(post_df, value_men)
ax3.barh(post_df, value_women, height=0.5)
plt.yticks(rotation=45, fontsize=5)
plt.legend(data_df['Пол'].unique(), loc=1)
plt.xlabel('Кол-во сотрудников разного пола')
plt.ylabel('Должности')
plt.title("Кол-во мужчин/женщин на должностях")
plt.show()

count_employees_in_year = data_df['Год начала работы в компании'].value_counts().sort_index()
plt.plot(count_employees_in_year.index, count_employees_in_year)
plt.xlabel('Года')
plt.ylabel('Кол-во людей, взятых на работу')
plt.title("Кол-во персонала, принятого в определенный год")
plt.show()