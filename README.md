# Minnow-Shark-Life-Game

It is a predator-prey model of the dynamics of fish minnows and sharks. Assume that the birth rate of the fish is independent of the number of sharks, and that each shark kills a number of fish proportional to their number. We also assume that the number of offspring produced by each shark is proportional to the number of fish eaten by the shark and also death rate of the sharks is constant. Then the dynamic follows these relations:

(1) dF(t) / dt = [b1 − d1 * S(t)] * F(t), <br>
(2) dS(t) / dt = [b2 * F(t) − d2] * S(t),<br>
F(t) is the number of fish at time t. <br>
S(t) is the number of sharks at time t. <br>
b1, d1, b2, d2 are parameters independent of F and S.

The eqations (1) and (2) are known as the *Lotka–Volterra* equations.
