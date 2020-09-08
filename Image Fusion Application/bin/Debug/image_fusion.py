import sys
import skimage, os
from skimage.color import rgb2hsv, hsv2rgb, rgb2gray
from skimage.transform import rescale, resize, rotate
import numpy as np
from skimage import io, feature, img_as_uint, morphology, transform, color, measure


global image_name

def get_saliency_map(img, height, width):
    img_grey = rgb2gray(io.imread("thermal//" + image_name + "_mask.bmp"))
    img_grey = resize(img_grey, (height, width))
    return img_grey


def replace_fusion(fusedImage, img_t, new_im, salient_image):
    threshold = float(sys.argv[5])
    for i in range(fusedImage.shape[0]):
        for j in range(fusedImage.shape[1]):
            if salient_image[i,j] > threshold:
                fusedImage[i][j] = img_t[i,j]
    return fusedImage

def saliency_fusion(fusedImage, img_t, new_im, salient_image):
    ratio = float(sys.argv[5])
    for i in range(fusedImage.shape[0]):
        for j in range(fusedImage.shape[1]):
            if salient_image[i,j] > 0:
                thermal = img_t[i][j] * salient_image[i][j] * ratio
                visible = new_im[i][j] * (1 - ratio)
                quotient = ((1 - ratio) + (salient_image[i][j] * ratio))
                fusedImage[i][j] = (thermal + visible) / quotient
    return fusedImage

def fusion(img_name,img_t,img_v, startX, startY, height, width):
    print(startX, startY, height, width)
    new_im = img_v[startX:startX + height, startY:startY + width]
    salient_image = np.array(get_saliency_map(img_t,height, width))

    fusedImage = np.array(new_im)

    methods = {'Replace': replace_fusion,
               'Saliency': saliency_fusion}

    fusion_method = sys.argv[4]
    fusedImage = methods[fusion_method](fusedImage, img_t, new_im, salient_image)

    for i in range(img_v.shape[0]):
        for j in range(img_v.shape[1]):
            if i > startX and j > startY and i < (startX + height) and j < (startY + width):
                img_v[i][j] = fusedImage[i - startX][j - startY]

    try:
        io.imsave("results/final_image_"+ img_name +".png",img_v)
    except:
        os.mkdir("results")
        io.imsave("results/final_image_" + img_name + ".png", img_v)

def get_coordinates(bmp_scale_x, bmp_scale_y, shape_list):
    width = (1 / bmp_scale_x) * shape_list[1]
    height = (1 / bmp_scale_y) * shape_list[0]
    start_x = (shape_list[3] - width) / 2
    start_y = shape_list[2] - height
    return int(start_x), int(start_y), int(width), int(height)

def get_correration_image(original_name,imgt_name,imgv_name, bmp_scale_x, bmp_scale_y):

    img_t = io.imread(imgt_name)
    img_v = io.imread(imgv_name)
    shape_list = [img_t.shape[0], img_t.shape[1], img_v.shape[0],img_v.shape[1]]
    start_x, start_y, width, height = get_coordinates(float(bmp_scale_x), float(bmp_scale_y), shape_list)
    img_t = (resize(img_t,(height,width)) * 255).astype(np.uint8)

    fusion(original_name,img_t, img_v, start_y, start_x, height, width)

image_name = sys.argv[1]
bmp_scale_x, bmp_scale_y = sys.argv[2], sys.argv[3]

get_correration_image(image_name,\
"thermal\\"+ image_name +"_.bmp","visible\\"+ image_name +".bmp",bmp_scale_x, bmp_scale_y)

