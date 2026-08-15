package com.unity3d.player;

public class UnityPermissions$ModalWaitForPermissionResponse implements IPermissionRequestCallbacks {
    private boolean haveResponse = false;

    @Override
    public synchronized void onPermissionDenied(String str) {
        this.haveResponse = true;
        notify();
    }

    @Override
    public synchronized void onPermissionDeniedAndDontAskAgain(String str) {
        this.haveResponse = true;
        notify();
    }

    @Override
    public synchronized void onPermissionGranted(String str) {
        this.haveResponse = true;
        notify();
    }

    public synchronized void waitForResponse() {
        try {
            if (this.haveResponse) {
                return;
            }
            wait();
        } catch (InterruptedException unused) {
        }
    }
}
