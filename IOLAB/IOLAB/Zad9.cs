using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOLAB
{
    class MatMulCalculator
    {
        public delegate void MatMulCompletedEventHandler(object sender, AsyncCompletedEventArgs e);
        private delegate void WorkerEventHandler(double[] mat1, double[] mat2, int size, AsyncOperation asyncOp);
        public event MatMulCompletedEventHandler MatMulCompleted;
        SendOrPostCallback onCompletedCallback;
        private HybridDictionary userStateToLifetime = new HybridDictionary();

        public MatMulCalculator() { }
        public void CalculateCompleted(object state) { }
        void Completion(int size, double[] mat, Exception ex, bool cancelled, AsyncOperation ao) { }
        bool TaskCancelled(object taskID) { return false; }
        void CalculateWorker(double[] mat1, double[] mat2, int size, AsyncOperation asyncOp) { }
        double getVal(double[] mat, int collumn, int row, int size) { return 0.0; }
        double[] MatMul(double[] mat1, double[] mat2, int size) { return new double[0]; }
        public virtual void MatMulAsync(double[] mat1, double[] mat2, int size, object taskId) { }
        public void CancelAsync(object taskId) { }
    }
    class Zad9
    {
        public void start()
        {

        }

        public int[,] MultiplyMatrix(int[,] a,int[,] b)
        {
            int[,] c;


            if (a.GetLength(1) == b.GetLength(0))
            {
                c = new int[a.GetLength(0), b.GetLength(1)];
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    for (int j = 0; j < c.GetLength(1); j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < a.GetLength(1); k++) 
                            c[i, j] = c[i, j] + a[i, k] * b[k, j];
                    }
                }
                return c;
            }
            else
            {
                return null;
            }
        }


    }







}
// NOTATKI / WNIOSKI
//TO DO