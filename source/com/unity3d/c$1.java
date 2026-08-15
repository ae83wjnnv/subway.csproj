package com.unity3d.player;

import android.hardware.camera2.CameraCaptureSession;
import android.hardware.camera2.CameraCaptureSession$CaptureCallback;
import android.hardware.camera2.CaptureFailure;
import android.hardware.camera2.CaptureRequest;
import android.hardware.camera2.TotalCaptureResult;

final class c$1 extends CameraCaptureSession$CaptureCallback {
    final c a;

    c$1(c cVar) {
        this.a = cVar;
    }

    @Override
    public final void onCaptureCompleted(CameraCaptureSession cameraCaptureSession, CaptureRequest captureRequest, TotalCaptureResult totalCaptureResult) {
        c.a(this.a, captureRequest.getTag());
    }

    @Override
    public final void onCaptureFailed(CameraCaptureSession cameraCaptureSession, CaptureRequest captureRequest, CaptureFailure captureFailure) {
        f.Log(5, "Camera2: Capture session failed " + captureRequest.getTag() + " reason " + captureFailure.getReason());
        c.a(this.a, captureRequest.getTag());
    }

    @Override
    public final void onCaptureSequenceAborted(CameraCaptureSession cameraCaptureSession, int i) {
    }

    @Override
    public final void onCaptureSequenceCompleted(CameraCaptureSession cameraCaptureSession, int i, long j) {
    }
}
