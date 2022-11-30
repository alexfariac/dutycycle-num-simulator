import matplotlib.pyplot as plt

fig, ax = plt.subplots()

#Torus(14,14); S2: Torus(14,14); lcm: 196; gcf: 196;  AVG-NDT: 85; MAX-NDT: 195
#Searchlight(20); S2: Torus(14,14); lcm: 9800; gcf: 4; AVG-NDT: 109; MAX-NDT: 662
#Disco(17, 19); S2: Torus(14,14); lcm: 63308; gcf: 1;  AVG-NDT: 63; MAX-NDT: 237
#Grid(19,19); S2: Torus(14,14); lcm: 70756; gcf: 1;  AVG-NDT: 82; MAX-NDT: 265
#Torus(14,14); S2: UConnect(13); lcm: 33124; gcf: 1;  AVG-NDT: 59; MAX-NDT: 181

methods = ["Torus(14,14)", "Searchlight(20)", "Disco(17, 19)", "Grid(19,19)", "U-Connect(13)"]
lcm = [196, 9800, 63308, 70756, 33124]
gcf = [196, 4, 1, 1, 1]
avg_ndt = [85, 109, 63, 82, 59]
max_ndt = [195, 662, 237, 265, 181]

bar_colors = ['tab:red', 'tab:blue', 'tab:blue', 'tab:blue', 'tab:blue']

ax.bar(methods, max_ndt, label=methods, color=bar_colors)

ax.set_ylabel('NDT máximo (Slots)')
ax.set_title('Torus(14,14)')
ax.legend(title='Método comparado')

outputImageFileName = "Torus(14,14)-MAX-NDT-Graph"
plt.savefig(outputImageFileName)