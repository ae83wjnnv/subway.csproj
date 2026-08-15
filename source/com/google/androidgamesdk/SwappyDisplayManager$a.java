package com.google.androidgamesdk;

import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import java.util.concurrent.locks.Condition;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

class SwappyDisplayManager$a extends Thread {
    public Handler a;
    final SwappyDisplayManager b;
    private Lock c;
    private Condition d;

    private SwappyDisplayManager$a(SwappyDisplayManager swappyDisplayManager) {
        this.b = swappyDisplayManager;
        ReentrantLock reentrantLock = new ReentrantLock();
        this.c = reentrantLock;
        this.d = reentrantLock.newCondition();
    }

    SwappyDisplayManager$a(SwappyDisplayManager swappyDisplayManager, byte b) {
        this(swappyDisplayManager);
    }

    @Override
    public final void run() {
        Log.i("SwappyDisplayManager", "Starting looper thread");
        this.c.lock();
        Looper.prepare();
        this.a = new Handler();
        this.d.signal();
        this.c.unlock();
        Looper.loop();
        Log.i("SwappyDisplayManager", "Terminating looper thread");
    }

    @Override
    public final void start() {
        this.c.lock();
        super.start();
        try {
            this.d.await();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        this.c.unlock();
    }
}
