package com.unity3d.player;

import android.media.Image;
import android.media.Image$Plane;
import android.media.ImageReader;
import android.media.ImageReader$OnImageAvailableListener;

final class c$4 implements ImageReader$OnImageAvailableListener {
    final c a;

    c$4(c cVar) {
        this.a = cVar;
    }

    @Override
    public final void onImageAvailable(ImageReader imageReader) {
        if (c.f().tryAcquire()) {
            Image imageAcquireNextImage = imageReader.acquireNextImage();
            if (imageAcquireNextImage != null) {
                Image$Plane[] planes = imageAcquireNextImage.getPlanes();
                if (imageAcquireNextImage.getFormat() == 35 && planes != null && planes.length == 3) {
                    c.h(this.a).a(planes[0].getBuffer(), planes[1].getBuffer(), planes[2].getBuffer(), planes[0].getRowStride(), planes[1].getRowStride(), planes[1].getPixelStride());
                } else {
                    f.Log(6, "Camera2: Wrong image format.");
                }
                if (c.i(this.a) != null) {
                    c.i(this.a).close();
                }
                c.a(this.a, imageAcquireNextImage);
            }
            c.f().release();
        }
    }
}
