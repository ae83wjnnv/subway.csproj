package com.unity3d.player;

import android.hardware.camera2.CameraDevice;
import android.hardware.camera2.CameraDevice$StateCallback;

final class c$3 extends CameraDevice$StateCallback {
    final c a;

    c$3(c cVar) {
        this.a = cVar;
    }

    @Override
    public final void onClosed(CameraDevice cameraDevice) {
        c.f().release();
    }

    @Override
    public final void onDisconnected(CameraDevice cameraDevice) {
        f.Log(5, "Camera2: CameraDevice disconnected.");
        c.b(this.a, cameraDevice);
        c.f().release();
    }

    @Override
    public final void onError(CameraDevice cameraDevice, int i) {
        f.Log(6, "Camera2: Error opeining CameraDevice " + i);
        c.b(this.a, cameraDevice);
        c.f().release();
    }

    @Override
    public final void onOpened(CameraDevice cameraDevice) {
        c.a(this.a, cameraDevice);
        c.f().release();
    }
}
