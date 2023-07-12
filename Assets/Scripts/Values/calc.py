import yaml
from yaml.loader import SafeLoader

fi = open('in.yaml', 'r')
values = yaml.load(fi, Loader=SafeLoader)

out = {}

ADD = values['global']['ADD']
kills_to_recover = values['global']['kills_to_recover']

for enemy_name in values['enemies']:
    enemy = values['enemies'][enemy_name]

    towers, akc = {}, 0
    for tower_name in values['towers']:
        tower = values['towers'][tower_name]

        tn = enemy['health'] * enemy['speed'] / (ADD  * tower['bullet_damage'] * tower['shooting_rate'])
        kc = tn * tower['cost']

        towers[tower_name] = {'needed': round(tn, 2), 'cost': round(kc, 2)}
        akc += kc
    akc /= len(values['towers'])

    for tower_name in values['towers']:
        towers[tower_name]['earning'] = round(akc / kills_to_recover, 2)

    killing_reward = round(akc / kills_to_recover, 2)

    out[enemy_name] = {'reward': killing_reward, 'towers': towers}

fo = open('out.yaml', 'w')
fo.write(yaml.dump(out))
