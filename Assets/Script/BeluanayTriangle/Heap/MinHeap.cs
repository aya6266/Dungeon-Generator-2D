using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


//When implemnting the heap, the 
//float heap need more practise to make a genric class
// public class MinHeap
// {
// //    private float capacity;
//     private int size;
//     private float[][] items;

//     private int currentSize;
//     public MinHeap(int _size)
//     {
//         //capacity = _capacity;
//         size = _size;
//         items = new float[size][];
//         currentSize = 0;
//     }

//     private int GetLeftChildIndex(int parentIndex){return 2 * parentIndex + 1;}
//     private int GetRightChildIndex(int parentIndex){return 2 * parentIndex + 2;}
//     private int GetParentIndex(int childIndex){return (childIndex - 1) / 2;}

//     private bool HasLeftChild(int index){return GetLeftChildIndex(index) < currentSize;}
//     private bool HasRightChild(int index){return GetRightChildIndex(index) < currentSize;}
//     private bool HasParent(int index){return GetParentIndex(index) >= 0;}

//     private float[] leftChild(int index){return items[GetLeftChildIndex(index)];}
//     private float[] rightChild(int index){return items[GetRightChildIndex(index)];}
//     private float[] parent(int index){return items[GetParentIndex(index)];}

//     private void Swap(int indexOne, int indexTwo)
//     {
//         float[] temp = items[indexOne];
//         items[indexOne] = items[indexTwo];
//         items[indexTwo] = temp;
//     }

//     // private void EnsureExtraCapacity()
//     // {
//     //     if(size == capacity)
//     //     {
//     //         capacity *= 2;
//     //         T[][] newItems = new T[capacity][];
//     //         for(int i = 0; i < size; i++)
//     //         {
//     //             newItems[i] = item[i];
//     //         }
//     //         item = newItems;
//     //     }
//     // }

//     public float[] Peek()
//     {
//         if(currentSize == 0) throw new System.Exception("Heap is empty");
//         return items[0];
//     }

//     public float[] Poll()
//     {
//         if(currentSize == 0) throw new System.Exception("Heap is empty");
//         float[] item = items[0];
//         items[0] = items[currentSize - 1];
//         currentSize--;
//         HeapifyDown();
//         return item;
//     }

//     public void Add(float[] item)
//     {
//         if(currentSize == size) throw new System.Exception("Heap is full");
//         //EnsureExtraCapacity();
//         items[currentSize] = item;
//         currentSize++;
        
//         HeapifyUp();
//     }

//     public void HeapifyUp()
//     {
//         int index = currentSize - 1;
//         while(HasParent(index) && parent(index)[0] > items[index][0])
//         {
//             Swap(GetParentIndex(index), index);
//             index = GetParentIndex(index);
//         }
//     }

//     public void HeapifyDown()
//     {
//         int index = 0;
//         while(HasLeftChild(index))
//         {
//             int smallerChildIndex = GetLeftChildIndex(index);
//             if(HasRightChild(index) && rightChild(index)[0] < leftChild(index)[0])
//             {
//                 smallerChildIndex = GetRightChildIndex(index);
//             }

//             if(items[index][0] < items[smallerChildIndex][0])
//             {
//                 break;
//             }
//             else
//             {
//                 Swap(index, smallerChildIndex);
//             }
//             index = smallerChildIndex;
//         }
//     }

//     public int GetSize()
//     {
//         return currentSize;
//     }

//     public float[][] GetItems()
//     {
//         return items;
//     }
// }
