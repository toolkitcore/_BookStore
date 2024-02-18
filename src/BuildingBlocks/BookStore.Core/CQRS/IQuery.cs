﻿using MediatR;

namespace BookStore.Core.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>;