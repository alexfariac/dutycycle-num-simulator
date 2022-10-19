#!/usr/bin/python

##
# This program finds Singer Difference Sets with lambda = 1. Singer Difference
# sets are those with parameters:
#
# ((q^m - 1) / (q - 1), (q^(m-1) - 1) / (q - 1), (q^(m-2) - 1) / (q - 1)),
#
# where q is a prime power and m >= 3 is a positive integer. Since we want
# lambda = 1, we need m = 3.
#
# The method employed here is based on Theorem 4.32 found on the book "Codes
# from Difference Sets" by Cunsheng Ding. The theorem basically states that
# we can determine the difference set by listing the elements alpha^i from GF(q^m)
# for which Tr_{q^m/q}(alpha^i) = 0.
#
# Thus, we basically have to construct the Galois Field GF(q) and, for each of
# its elements, apply the trace function mapping it back to the ground field. If
# the result of this mapping is zero, the exponent of the element (i.e., the 'i'
# in alpha_i) becomes an element of the difference set.
#
# Version history:
#  v2: speed-up the trace execution by using the pre-computed table of elements
# for the highest powers and use a repeated square method for reaching the remainder
# of the exponent (instead of simply raising the base poly to a very high power,
# resulting in a very large number of terms to be simplified).
#  v3: changed the fastPower function to receive an arbitrary base as a starting
# point. This way, the trace function can always supply the last term as a
# starting point which is always raised to the q-th power (by repeated squaring).
#  v4: new slight modification to the fastPower function, trying to reduce the
# number of steps.
#  v5: the exponentiation still seems to be the bottleneck. In this version we
# try to cache large powers already computed.

import sys
from sympy import *


##
# Some helper functions
def fastPower(base, e, G, p):
    # base is the polynomial to be raised, e is the target exponent, G is
    # the irreducible polynomial that generated the field.

    result = Poly(base)
    residue = Poly(1, x, modulus=p)

    while e > 1:
        if e & 1:
            residue = residue.mul(result).rem(G)
            e = e - 1
        result = result.mul(result).rem(G)
        e = e / 2

    return result.mul(residue).rem(G)

##
# Read the command line arguments. We just need the value of q.
if len(sys.argv) < 2:
    print("Usage: {0} <q>".format(sys.argv[0]))
    print("Where v = (q^3 - 1) / (q - 1) and q is a prime power.")
    sys.exit(1)

q = int(sys.argv[1])
v = (q**3 - 1) / (q - 1)
k = q + 1

##
# 1) Value q may not be prime. In that case, it cannot be the order of the
# ground field. We need to factor it to obtain the ground order for sure. We'll
# name it 'p'. We also have to store the exponent, that we will call m. Remember
# though that m is 3 * log_p(q).
PrimeFactors = factorint(q)
if len(PrimeFactors) > 1:
    print("Error: q is not a prime power!")
    sys.exit(2)

p = PrimeFactors.keys()[0]
m = 3 * PrimeFactors[p]

print("Looking for a difference set with parameters ({0}, {1}, 1)...".format(v, k))
sys.stdout.write("{0} {1} 1 ".format(v, k))

##
# 2) To construct the GF(p^m), we need a irreducible polynomial of degree m in
# GF(p). We'll use a randomized algorithm for that. It expected run time is
# polynomial in m, and m should not be very large for practical applications.
x = Symbol('x')
while True:
    G = Poly(polys.specialpolys.random_poly(x, m, 0, p-1), modulus=p)
    if G.is_irreducible:
        break

##
# 3) Build the GF(p^m). Strictly speaking, we could do skip this phase, but
# pre-computing it might be useful. We store each non-null element in a list.
ExtendedField = list()

# The first m elements are easy: they are simply the first few powers of x
ExtendedField = {}
for i in range(m):
    ExtendedField[i] = Poly(x**i, x, modulus=p)

# The next elements are a bit more complicated: we take the previously generated
# element, and we multiply by x^1. But we have to make sure the result is computed
# module the generator.
for i in range(m, v):
    ExtendedField[i] = ExtendedField[1].mul(ExtendedField[i-1]).rem(G)

##
# 4) Iterate through each i-th element of the GF(p^m) applying the Trace function.
# If it's zero, output the new found element in D.
for i in range(v):
    term = Poly(ExtendedField[i])
    trace = Poly(term)
    exponent = q*i
    for j in range(2):

        if exponent in ExtendedField:
            term = ExtendedField[exponent]
        else:
            term = fastPower(term, q, G, p)
            ExtendedField[exponent] = term

        trace = trace + term
        exponent = exponent * q

    trace = trace.rem(G)

    if trace == 0:
        sys.stdout.write("{0} ".format(i))
        k = k - 1
        if k == 0:
            break

print("")

sys.stdout.flush()
