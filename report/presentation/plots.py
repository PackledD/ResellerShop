import matplotlib.pyplot as plt

x = [1000, 5000, 10000, 50000, 100000]
y1 = [
    [174, 171, 178, 184, 187],
    [166, 185, 194, 199, 212],
    [172, 177, 195, 205, 223]
]
y2 = [
    [162, 161, 170, 172, 176],
    [156, 163, 177, 188, 190],
    [153, 167, 177, 190, 193]
]
f1 = plt.figure()
s1 = f1.add_subplot(1, 1, 1)
for i in range(3):
    s1.plot(x, y1[i], "-o")
f2 = plt.figure()
s2 = f2.add_subplot(1, 1, 1)
for i in range(3):
    s2.plot(x, y2[i], "-o")
    
s1.set_title("Без доп. индекса")
s1.legend(["Поле id", "Поле email", "Оба поля"])
s1.set_xticks(x, x, rotation=45)
s1.set_xlabel("К-во записей")
s1.set_ylabel("Время, мс.")

s2.set_title("С доп. индексом")
s2.legend(["Поле id", "Поле email", "Оба поля"])
s2.set_xticks(x, x, rotation=45)
s2.set_xlabel("К-во записей")
s2.set_ylabel("Время, мс.")

plt.show()
