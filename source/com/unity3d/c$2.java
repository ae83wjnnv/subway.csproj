package com.unity3d.player;

import android.hardware.camera2.CameraAccessException;
import android.hardware.camera2.CameraCaptureSession;
import android.hardware.camera2.CameraCaptureSession$StateCallback;
import android.hardware.camera2.CaptureRequest;

final class c$2 extends CameraCaptureSession$StateCallback {
    final c a;

    c$2(c cVar) {
        this.a = cVar;
    }

    @Override
    public final void onConfigureFailed(CameraCaptureSession cameraCaptureSession) {
        f.Log(6, "Camera2: CaptureSession configuration failed.");
    }

    @Override
    public final void onConfigured(CameraCaptureSession cameraCaptureSession) {
        if (c.a(this.a) == null) {
            return;
        }
        synchronized (c.b(this.a)) {
            c.a(this.a, cameraCaptureSession);
            try {
                c.a(this.a, c.a(this.a).createCaptureRequest(1));
                if (c.c(this.a) != null) {
                    c.d(this.a).addTarget(c.c(this.a));
                }
                c.d(this.a).addTarget(c.e(this.a).getSurface());
                c.d(this.a).set(CaptureRequest.CONTROL_AE_TARGET_FPS_RANGE, c.f(this.a));
                c.g(this.a);
            } catch (CameraAccessException e) {
                f.Log(6, "Camera2: CameraAccessException " + e);
            }
        }
    }
}
