import matplotlib.pyplot as plt

def addlabels(x,y):
    for i in range(len(x)):
        plt.text(i,y[i],y[i])

fig, ax = plt.subplots()

#Disco(17, 19); S2: Disco(17, 19); lcm: 323; gcf: 323;  AVG-NDT: 96; MAX-NDT: 321 
#Searchlight(20); S2: Disco(17, 19); lcm: 64600; gcf: 1; AVG-NDT: 71; MAX-NDT: 339
#Disco(17, 19); S2: Grid(19,19); lcm: 6137; gcf: 19;  AVG-NDT: 102; MAX-NDT: 322
#Disco(17, 19); S2: Torus(14,14); lcm: 63308; gcf: 1;  AVG-NDT: 63; MAX-NDT: 237
#Disco(17, 19); S2: UConnect(13); lcm: 54587; gcf: 1;  AVG-NDT: 61; MAX-NDT: 220

methods = ["Disco(17, 19)", "Searchlight(20)", "Grid(19,19)", "Torus(14,14)", "U-Connect(13)"]
lcm = [323, 64600, 6137, 63308, 54587]
gcf = [323, 1, 19, 1, 1]
avg_ndt = [96, 71, 102, 63, 61]
max_ndt = [321, 339, 322, 237, 220]

bar_colors = ['tab:red', 'tab:blue', 'tab:blue', 'tab:blue', 'tab:blue']

ax.bar(methods, max_ndt, label=methods, color=bar_colors)
#addlabels(methods, max_ndt)

ax.set_ylabel('NDT máximo (Slots)')
ax.set_title('Disco(17,19)')
ax.legend(title='Método comparado')

outputImageFileName = "Disco(17,19)-MAX-NDT-Graph"
plt.savefig(outputImageFileName, bbox_inches='tight')