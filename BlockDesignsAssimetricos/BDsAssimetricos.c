#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdint.h>

#define SAFE_MALLOC(var, type, size)   do {\
                                            var = (type *) malloc(size);\
                                            if (var == NULL) {\
                                                fprintf(stderr, "Out of memory: failed to allocate %lu bytes for variable '%s' at %s:%d\n", \
                                                        (long unsigned int) size, #var, __FILE__, __LINE__);\
                                                exit(1);\
                                            }\
                                        } while(0);

#define SAFE_REALLOC(var, type, size)   do {\
                                                var = (type *) realloc(var, size);\
                                                if (var == NULL) {\
                                                    fprintf(stderr, "Out of memory: failed to reallocate %lu bytes for variable '%s' at %s:%d\n", \
                                                            (long unsigned int) size, #var, __FILE__, __LINE__);\
                                                    exit(1);\
                                                }\
                                        } while(0);

typedef struct {

    unsigned int bufferSize;
    unsigned int bitmapLength;
    uint64_t buffer[0]; 
} bitmap_t;


bitmap_t * bitmapNew(unsigned int bitmapLength) {

    bitmap_t * p;
    unsigned int bufferSize;

    bufferSize = bitmapLength / (8 * sizeof(uint64_t));
    if (bitmapLength % (8 * sizeof(uint64_t)) != 0) bufferSize++;

    SAFE_MALLOC(p, bitmap_t, sizeof(bitmap_t) + sizeof(uint64_t) * bufferSize);
    memset(p->buffer, 0, sizeof(uint64_t) * bufferSize);
    p->bufferSize = bufferSize;
    p->bitmapLength = bitmapLength;

    return(p);
}

void bitmapFree(bitmap_t * p) {

    free(p);
}

void bitmapAdd(bitmap_t * p, unsigned int element) {

    unsigned int bufferPage;
    unsigned int pageIndex;

    bufferPage = element / (8 * sizeof(uint64_t));
    pageIndex = element % (8 * sizeof(uint64_t));

    p->buffer[bufferPage] |= (1ul << pageIndex);
//printf("element = %u, bufferPage = %u, pageIndex = %u, page content = %lu\n", element, bufferPage, pageIndex, p->buffer[bufferPage]);
}

uint64_t bitmapIsSet(bitmap_t * p, unsigned int element) {

    unsigned int bufferPage;
    unsigned int pageIndex;

    bufferPage = element / (8 * sizeof(uint64_t));
    pageIndex = element % (8 * sizeof(uint64_t));
//printf("bufferPage = %u, pageIndex = %u, page content = %lu\n", bufferPage, pageIndex, p->buffer[bufferPage]);
    return(p->buffer[bufferPage] & (1ul << pageIndex));
}

typedef struct {

    unsigned int v;
    unsigned int k;
    unsigned int filledActiveSlots;
    bitmap_t * activeSlotsBitmap;
    unsigned int activeSlots[0];
} bd_t;

bd_t * bdNew(int v, int k) {

    bd_t * p;

    SAFE_MALLOC(p, bd_t, sizeof(bd_t) + sizeof(unsigned int) * k);
    p->v = v;
    p->k = k;
    p->filledActiveSlots = 0;
    p->activeSlotsBitmap = bitmapNew(v);

    return(p);
}

void bdAddActiveSlot(bd_t * p, unsigned int slot) {

    p->activeSlots[p->filledActiveSlots++] = slot;
    bitmapAdd(p->activeSlotsBitmap, slot);
}

unsigned int bdGetActiveSlot(bd_t * p, unsigned int index) {

    return(p->activeSlots[index]);
}

uint64_t bdIsSlotActive(bd_t * p, unsigned int slot) {

    return(bitmapIsSet(p->activeSlotsBitmap, slot));
}

void bdFree(bd_t * p) {

    bitmapFree(p->activeSlotsBitmap);
    free(p);
}

typedef struct {

    unsigned int bufferSize;
    unsigned int length;
    uint64_t values[0]; // Actually, this is a max heap.
} sortedArray_t;

sortedArray_t * sortedArrayNew() {

    sortedArray_t * p;

    SAFE_MALLOC(p, sortedArray_t, sizeof(sortedArray_t) + 100 * sizeof(uint64_t));
    p->bufferSize = 100;
    p->length = 0;

    return(p);
}

void sortedArrayAdd(sortedArray_t ** p, uint64_t value) {

    unsigned int current, parent;

    if ((* p)->bufferSize == (* p)->length) {

        SAFE_REALLOC((* p), sortedArray_t, sizeof(sortedArray_t) + ((* p)->bufferSize + 100) * sizeof(uint64_t));
        (* p)->bufferSize += 100;
    }

    current = (* p)->length++;
    (* p)->values[current] = value;
    
    parent = (current - 1) / 2;
    while(current > 0 && (* p)->values[parent] < (* p)->values[current]) {

        (* p)->values[current] = (* p)->values[parent];
        (* p)->values[parent] = value;
        current = parent;
        parent = (current - 1) / 2;
    }
}

uint64_t sortedArrayGet(sortedArray_t * p, unsigned int index) {

    return(p->values[index]);
}

void sortedArrayReset(sortedArray_t * p) {

    p->length = 0;
}

void sortedArrayFree(sortedArray_t * p) {

    free(p);
}

unsigned int sortedArrayLength(sortedArray_t * p) {

    return(p->length);
}

void sortedArraySort(sortedArray_t * p) {

    unsigned int remainingValues;
    uint64_t nextMax;
    uint64_t bubble;
    unsigned int current, child1, child2;

    // We will extract the maximum element of the heap for all elements. At the 
    // end of the process, the array that holds the heap should be ordered.
    for (remainingValues = p->length; remainingValues > 1; remainingValues--) {

        // Extract the maximum remaining element:
        // 1) Swap the maximum (at index 0) with the last (of the remaining)
        nextMax = p->values[0];
        p->values[0] = p->values[remainingValues - 1];
        p->values[remainingValues - 1] = nextMax;
/*
printf("This is the content of intersections after max extraction: ");
for (int j = 0; j < sortedArrayLength(p); j++) {
    printf("%u ", sortedArrayGet(p, j));
}		
printf("\n");*/
        // 2) Fix the heap by bubbling the element at index 0 down.
        bubble = p->values[0];
        current = 0;
        while(current < remainingValues - 1) {

            child1 = current * 2 + 1;
            child2 = child1 + 1;
//printf("bubble = %u, child1 = %u, child2 = %u, p->values[child1] = %u, p->values[child2] = %u\n", bubble, child1, child2, p->values[child1], p->values[child2]);
            if ((child1 < remainingValues - 1 && bubble < p->values[child1]) || (child2 < remainingValues - 1 && bubble < p->values[child2])) {

                if (child2 >= remainingValues - 1 || p->values[child1] >= p->values[child2]) {

                    p->values[current] = p->values[child1];
                    current = child1;
                }
                else {

                    p->values[current] = p->values[child2];
                    current = child2;                    
                }
            }
            else {

                break ;
            }
/*            
printf("This is the content of intersections after one bubble down: ");
for (int j = 0; j < sortedArrayLength(p); j++) {
    printf("%u ", sortedArrayGet(p, j));
}		
printf("\n");    
*/
        }

        p->values[current] = bubble;
/*
printf("This is the content of intersections after all bubble downs: ");
for (int j = 0; j < sortedArrayLength(p); j++) {
    printf("%u ", sortedArrayGet(p, j));
}		
printf("\n");  */     
    }
}


typedef struct {

    unsigned int bufferSize;
    unsigned int length;
    void * values[0]; // Actually, this is a max heap.
} array_t;

array_t * arrayNew() {

    array_t * p;

    SAFE_MALLOC(p, array_t, sizeof(array_t) + 100 * sizeof(void *));
    p->bufferSize = 100;
    p->length = 0;

    return(p);
}

void arrayAdd(array_t ** p, void * value) {

    if ((* p)->bufferSize == (* p)->length) {

        SAFE_REALLOC((* p), array_t, sizeof(array_t) + ((* p)->bufferSize + 100) * sizeof(void *));
        (* p)->bufferSize += 100;
    }

    (* p)->values[(* p)->length++] = value;
}

void * arrayGet(array_t * p, unsigned int index) {

    return(p->values[index]);
}

void arrayReset(array_t * p) {

    p->length = 0;
}

unsigned int arrayLength(array_t * p) {

    return(p->length);
}

void arrayFree(array_t * p) {

    free(p);
}

array_t * readInput() {

    unsigned int v, k, i;
    unsigned int slot;
    array_t * listOfBDs = arrayNew();
    bd_t * nextBd;

    /*
     * Read input file from stdin.
     */

    /*
     * Read the next BD. The line should be in the format 'v k l s0 s1 ...'
     */

    fscanf(stdin, "%u %u %*d", & v, & k);

    while (!feof(stdin)) {

        nextBd = bdNew(v, k);

//printf("Storing BD with v = %u and k = %u\n", v, k);

        for (i = 0; i < k; i++) {

            fscanf(stdin, "%u", & slot);
            bdAddActiveSlot(nextBd, slot);
        }

        arrayAdd(& listOfBDs, nextBd);

        /*
        * Read the next BD. The line should be in the format 'v k l s0 s1 ...'
        */

        fscanf(stdin, "%u %u %*d", & v, & k);
    }

    return(listOfBDs);
}

unsigned int gcd(unsigned a, unsigned int b) {

    if (b == 0) return(a);
	return(gcd(b, a % b));
}

void NDT(bd_t * bd1, bd_t * bd2) {

    unsigned int g;
    uint64_t MTTR;
    uint64_t v;
    unsigned int o;
    unsigned int i;
    unsigned int r;
    unsigned int s;
    uint64_t sAbs;
    uint64_t currentIntersection, nextIntersection;
    uint64_t sum, diff, sumOverAllOffsets;
    sortedArray_t * intersections;

    g = gcd(bd1->v, bd2->v);
    printf("%d %d %d %.6f %.6f", bd1->v, bd2->v, g, bd1->k / (double) bd1->v, bd2->k / (double) bd2->v);

    MTTR = 0;
	sumOverAllOffsets = 0;
	v = ((uint64_t) bd1->v) * bd2->v;
//printf("sAbs = %lu, v = %lu\n", sAbs, v);
    intersections = sortedArrayNew();
	for (o = 0; o < g; o++) {

		sortedArrayReset(intersections);
		for (i = 0; i < bd2->k; i++) {
//printf("\t- Trying to match active slot %u from bd2\n", bdGetActiveSlot(bd2, i));
			sAbs = (bdGetActiveSlot(bd2, i) + bd2->v + o) % v;
			for (r = 0; r < bd1->v/g; r++) {

				s = sAbs % bd1->v;
//printf("Is %u active in bd1?\n", s);               
				if (bdIsSlotActive(bd1, s)) {
//printf("Adding %lu to intersections because %u is in bd1 and %u is in bd2\n", sAbs, s, bdGetActiveSlot(bd2, i));
					sortedArrayAdd(& intersections, sAbs);
				}
				sAbs = (sAbs + bd2->v) % v;
                //printf("sAbs = %lu, v = %lu\n", sAbs, v);
			}
		}

		if (sortedArrayLength(intersections) == 0) {

			printf(" inf inf\n");
			return ;
		}
/*
printf("This is the content of intersections before sorting: ");
for (int j = 0; j < sortedArrayLength(intersections); j++) {
    printf("%u ", sortedArrayGet(intersections, j));
}		
printf("\n");*/

		sortedArraySort(intersections);

/*        
printf("This is the content of intersections after sorting: ");
for (int j = 0; j < sortedArrayLength(intersections); j++) {
    printf("%u ", sortedArrayGet(intersections, j));
}*/		
printf("\n");
		
//printf("Total intersections: %u\n", sortedArrayLength(intersections));
		sum = 0;
		for (i = 0; i < sortedArrayLength(intersections); i++) {
			
			currentIntersection = sortedArrayGet(intersections, i);
			if (i == sortedArrayLength(intersections) - 1) {

				nextIntersection = sortedArrayGet(intersections, 0);
				diff = nextIntersection - currentIntersection + v/g;
			}
			else {

				nextIntersection = sortedArrayGet(intersections, i + 1);
				diff = nextIntersection - currentIntersection;
			}

			sum += ((diff - 1) * diff) / 2;
//printf("sum = %lu\n", sum);        
printf("Diff = %lu\n", diff);
			if (MTTR < diff) {

				MTTR = diff;
			}
		}

		sumOverAllOffsets += sum;
	}

	printf(" %.6f %lu\n", (double) sumOverAllOffsets / v, MTTR);
//fflush(stdout);
}

int main() {

    array_t * listOfBDs;
    unsigned int i, j;
    unsigned int numberOfBDs;

    listOfBDs = readInput();
    numberOfBDs = arrayLength(listOfBDs);

    for (i = 0; i < 4; i++) {
    //for (i = 0; i < numberOfBDs - 1; i++) {

        for (j = i + 1; j < numberOfBDs; j++) {

//            NDT(arrayGet(listOfBDs, i), arrayGet(listOfBDs, i));
            NDT(arrayGet(listOfBDs, i), arrayGet(listOfBDs, j));
        }
    }

    return(0);
}
