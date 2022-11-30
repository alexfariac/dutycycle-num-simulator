import matplotlib.pyplot as plt

fig, ax = plt.subplots()

#Grid(19,19); S2: Grid(19,19); lcm: 361; gcf: 361; AVG-NDT: 113; MAX-NDT: 359
#Searchlight(20); S2: Grid(19,19); lcm: 72200; gcf: 1; AVG-NDT: 95; MAX-NDT: 367
#Disco(17, 19); S2: Grid(19,19); lcm: 6137; gcf: 19; AVG-NDT: 102; MAX-NDT: 322
#Grid(19,19); S2: Torus(14,14); lcm: 70756; gcf: 1; AVG-NDT: 82; MAX-NDT: 265
#Grid(19,19); S2: UConnect(13); lcm: 61009; gcf: 1; AVG-NDT: 80; MAX-NDT: 246

methods = ["Grid(19,19)", "Searchlight(20)", "Disco(17, 19)", "Torus(14,14)", "U-Connect(13)"]
lcm = [361, 72200, 6137, 70756, 61009]
gcf = [323, 1, 19, 1, 1]
avg_ndt = [113, 95, 102, 82, 80]
max_ndt = [359, 367, 322, 265, 246]

bar_colors = ['tab:red', 'tab:blue', 'tab:blue', 'tab:blue', 'tab:blue']

ax.bar(methods, max_ndt, label=methods, color=bar_colors)

ax.set_ylabel('NDT máximo (Slots)')
ax.set_title('Grid(19,19)')
ax.legend(title='Método comparado')

outputImageFileName = "Grid(19,19)-MAX-NDT-Graph"
plt.savefig(outputImageFileName, bbox_inches='tight')