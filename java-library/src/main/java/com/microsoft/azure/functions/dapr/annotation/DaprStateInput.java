/**
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 */

package com.microsoft.azure.functions.dapr.annotation;

import java.lang.annotation.Retention;
import java.lang.annotation.Target;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.ElementType;

import com.microsoft.azure.functions.annotation.CustomBinding;

/**
 * Attribute to specify parameters for the dapr-state input binding.
 */
@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.PARAMETER)
@CustomBinding(direction = "in", name = "daprStateInputMessage", type = "daprState")
public @interface DaprStateInput {
    /**
     * Dapr runtime endpoint.
     */
    String daprAddress() default "";

    /**
     * Name of the state store to retrieve or store state.
     */
    String stateStore() default "";

    /**
     * Key name to get or set state.
     */
    String key() default "";
}
